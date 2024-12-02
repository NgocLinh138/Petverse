using Application.Usecases.V1.VaccineRecommendation.Commands;
using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Exceptions.Abstractions;
using Contract.JsonConverters;
using Contract.Services.V1.Pet;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Hangfire;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.Pet.Commands
{
    public sealed class CreatePetCommandHandler : ICommandHandler<Command.CreatePetCommand, Responses.PetResponse>
    {
        private readonly IBlobStorageService blobStorageService;
        private readonly IPetRepository petRepository;
        private readonly IPhotoRepository petPhotoRepository;
        private readonly IBreedRepository breedRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly UpdateVaccineRecommendationCommandHandler updateVaccineRecommendationCommandHandler;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreatePetCommandHandler(
            IBlobStorageService blobStorageService,
            IPetRepository petRepository,
            IPhotoRepository petPhotoRepository,
            IBreedRepository breedRepository,
            ISpeciesRepository speciesRepository,
            UserManager<AppUser> userManager,
            IBackgroundJobClient backgroundJobClient,
            UpdateVaccineRecommendationCommandHandler updateVaccineRecommendationCommandHandler,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.blobStorageService = blobStorageService;
            this.petRepository = petRepository;
            this.petPhotoRepository = petPhotoRepository;
            this.breedRepository = breedRepository;
            this.speciesRepository = speciesRepository;
            this.userManager = userManager;
            this.backgroundJobClient = backgroundJobClient;
            this.updateVaccineRecommendationCommandHandler = updateVaccineRecommendationCommandHandler;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetResponse>> Handle(Command.CreatePetCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null || user.IsDeleted)
                return Result.Failure<Responses.PetResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

            var species = await speciesRepository.FindByIdAsync(request.SpeciesId, cancellationToken);
            if (species == null)
                return Result.Failure<Responses.PetResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

            var breed = await breedRepository.FindByIdAsync(request.BreedId, cancellationToken);
            if (breed == null || breed.SpeciesId != request.SpeciesId)
                return Result.Failure<Responses.PetResponse>("Giống thú cưng không tìm thấy hoặc không hợp lệ.", StatusCodes.Status404NotFound);

            string? avatarUrl = null;
            if (request.Avatar != null)
            {
                avatarUrl = await UploadPetImage(request.Avatar);
                if (avatarUrl == null) return Result.Failure<Responses.PetResponse>("Hình ảnh không thể để trống.", StatusCodes.Status400BadRequest);
            }

            try
            {

                var pet = new Domain.Entities.Pet
                {
                    UserId = request.UserId,
                    BreedId = request.BreedId,
                    Name = request.Name,
                    BirthDate = DateTimeConverters.StringToDate(request.BirthDate).Value,
                    Gender = request.Gender,
                    Weight = request.Weight,
                    Sterilized = request.Sterilized,
                    Avatar = avatarUrl,
                    Description = request.Description
                };

                await petRepository.AddAsync(pet);
                await unitOfWork.SaveChangesAsync();

                backgroundJobClient.Enqueue(() => updateVaccineRecommendationCommandHandler.UpdateVaccineRecommendationsAsync(pet.Id));

                List<Responses.PetPhotoResponse> petMediaUrls = new List<Responses.PetPhotoResponse>();
                if (request.PetPhotos != null && request.PetPhotos.Count > 0 || request.PetVideos != null)
                {
                    petMediaUrls = await AddPetMedia(pet.Id, request.PetPhotos, request.PetVideos);
                }

                var response = mapper.Map<Responses.PetResponse>(pet);
                response.PetPhotos = petMediaUrls;

                return Result.Success(response, 201);
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(avatarUrl))
                {
                    await blobStorageService.DeleteBlobSnapshotsAsync(avatarUrl);
                }
                throw;
            }
        }

        private async Task<string?> UploadPetImage(IFormFile? image)
        {
            if (image == null) return null;

            var firstName = image.FileName.Split(' ').FirstOrDefault() ?? "PetImage";
            var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

            return await blobStorageService.UploadBlob(image: image, name: nameImage, folder: BlobFolder.pets);
        }

        private async Task<List<Responses.PetPhotoResponse>> AddPetMedia(int petId, ICollection<IFormFile>? petPhotos, ICollection<IFormFile>? petVideos)
        {
            List<Responses.PetPhotoResponse> listMedia = new List<Responses.PetPhotoResponse>();
            Random random = new Random();

            // Handle images
            if (petPhotos != null)
            {
                foreach (var photo in petPhotos)
                {
                    if (!IsImageByExtension(photo))
                    {
                        throw new BadRequestException("Mỗi ảnh tải lên phải có định dạng hợp lệ (jpg, jpeg, png).");
                    }

                    string fileName = $"{petId}-photo-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(photo.FileName)}";
                    string urlString = await blobStorageService.UploadBlob(image: photo, name: fileName, folder: BlobFolder.pets);

                    var petPhoto = new PetImage
                    {
                        PetId = petId,
                        Type = MediaType.Image,
                        URL = urlString
                    };

                    await petPhotoRepository.AddAsync(petPhoto);
                    await unitOfWork.SaveChangesAsync();

                    listMedia.Add(new Responses.PetPhotoResponse
                    {
                        PetPhotoId = petPhoto.Id,
                        Url = urlString,
                        Type = MediaType.Image
                    });
                }
            }

            // Handle videos
            if (petVideos != null)
            {
                foreach (var video in petVideos)
                {
                    if (!IsVideoByExtension(video))
                    {
                        throw new BadRequestException("Mỗi video tải lên phải có định dạng hợp lệ (.mp4, .avi, .wmv, .mpeg).");
                    }

                    string fileName = $"{petId}-video-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(video.FileName)}";
                    string urlString = await blobStorageService.UploadBlob(image: video, name: fileName, folder: BlobFolder.pets);

                    var petVideo = new PetImage
                    {
                        PetId = petId,
                        URL = urlString,
                        Type = MediaType.Video
                    };

                    await petPhotoRepository.AddAsync(petVideo);
                    await unitOfWork.SaveChangesAsync();

                    listMedia.Add(new Responses.PetPhotoResponse
                    {
                        PetPhotoId = petVideo.Id,
                        Url = urlString,
                        Type = MediaType.Video
                    });
                }
            }

            return listMedia;
        }

        private bool IsImageByExtension(IFormFile file)
        {
            string[] validImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string extension = Path.GetExtension(file.FileName).ToLower();
            long maxFileSize = 10 * 1024 * 1024; // 10MB

            if (file.Length > maxFileSize)
            {
                throw new BadRequestException("Tệp hình ảnh quá lớn. Dung lượng tối đa là 10MB.");
            }

            return validImageExtensions.Contains(extension);
        }

        private bool IsVideoByExtension(IFormFile file)
        {
            string[] validVideoExtensions = { ".mp4", ".avi", ".wmv", ".mpeg" };
            string extension = Path.GetExtension(file.FileName).ToLower();
            long maxFileSize = 50 * 1024 * 1024; // 50MB

            if (file.Length > maxFileSize)
            {
                throw new BadRequestException("Tệp video quá lớn. Dung lượng tối đa là 50MB.");
            }

            return validVideoExtensions.Contains(extension);
        }
    }
}
