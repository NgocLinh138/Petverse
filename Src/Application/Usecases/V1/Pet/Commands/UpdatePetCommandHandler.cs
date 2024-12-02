using Application.Usecases.V1.VaccineRecommendation.Commands;
using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Exceptions.Abstractions;
using Contract.Services.V1.Pet;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Hangfire;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Pet.Commands
{
    public sealed class UpdatePetCommandHandler : ICommandHandler<Command.UpdatePetCommand, Responses.PetResponse>
    {
        private readonly IPetRepository petRepository;
        private readonly IPhotoRepository petPhotoRepository;
        private readonly IVaccineReccomendationRepository vaccineReccomendationRepository;
        private readonly IMapper mapper;
        private readonly IBlobStorageService blobStorageService;
        private readonly IBackgroundJobClient backgroundJobClient;
        private readonly UpdateVaccineRecommendationCommandHandler updateVaccineRecommendationCommandHandler;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePetCommandHandler(
            IPetRepository petRepository,
            IPhotoRepository petPhotoRepository,
            IVaccineReccomendationRepository vaccineReccomendationRepository,
            IMapper mapper,
            IBlobStorageService blobStorageService,
            IBackgroundJobClient backgroundJobClient,
            UpdateVaccineRecommendationCommandHandler updateVaccineRecommendationCommandHandler,
            IUnitOfWork unitOfWork)
        {
            this.petRepository = petRepository;
            this.petPhotoRepository = petPhotoRepository;
            this.vaccineReccomendationRepository = vaccineReccomendationRepository;
            this.mapper = mapper;
            this.blobStorageService = blobStorageService;
            this.backgroundJobClient = backgroundJobClient;
            this.updateVaccineRecommendationCommandHandler = updateVaccineRecommendationCommandHandler;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetResponse>> Handle(Command.UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await petRepository.FindByIdAsync(request.Id, cancellationToken);
            if (pet == null || pet.IsDeleted)
                return Result.Failure<Responses.PetResponse>("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

            var oldBirthDate = pet.BirthDate;

            pet.Update(request);    

            string? imageUrl = null;
            if (request.Avatar != null)
            {
                var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
                var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

                imageUrl = await blobStorageService.UploadBlob(image: request.Avatar, name: nameImage, folder: BlobFolder.pets)
                            ?? throw new Exception("Không thể tải tệp lên.");

                pet.Avatar = imageUrl;
            }

            await HandlePetPhotos(request, pet.Id);
            await unitOfWork.SaveChangesAsync();

            if (oldBirthDate != pet.BirthDate)
            {
                var existingRecommendations = vaccineReccomendationRepository.FindAll(r => r.PetId == pet.Id);
                foreach (var recommendation in existingRecommendations)
                {
                    vaccineReccomendationRepository.Remove(recommendation);
                }

                backgroundJobClient.Enqueue(() => updateVaccineRecommendationCommandHandler.UpdateVaccineRecommendationsAsync(pet.Id));
            }

            var response = mapper.Map<Responses.PetResponse>(pet);

            return Result.Success(response, 202);
        }

        private async Task HandlePetPhotos(Command.UpdatePetCommand request, int petId)
        {
            // Delete existing photos
            if (request.PetPhotosToDelete != null && request.PetPhotosToDelete.Any())
            {
                var existingPhotos = await petPhotoRepository.GetPhotosByPetIdAsync(petId);
                foreach (var photoId in request.PetPhotosToDelete)
                {
                    var petPhoto = existingPhotos.FirstOrDefault(x => x.Id == photoId);
                    if (petPhoto != null)
                    {
                        await blobStorageService.DeleteBlobSnapshotsAsync(petPhoto.URL);
                        petPhotoRepository.Remove(petPhoto);
                    }
                }
            }

            // Add new photos and videos
            if (request.PetPhotos?.Any() == true || request.PetVideos?.Any() == true)
            {
                await UpdatePetMedia(petId, request.PetPhotos, request.PetVideos);
            }
        }

        private async Task<List<Responses.PetPhotoResponse>> UpdatePetMedia(int petId, ICollection<IFormFile>? petPhotos, ICollection<IFormFile>? petVideos)
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
            return validImageExtensions.Contains(extension);
        }

        private bool IsVideoByExtension(IFormFile file)
        {
            string[] validVideoExtensions = { ".mp4", ".avi", ".wmv", ".mpeg" };
            string extension = Path.GetExtension(file.FileName).ToLower();
            return validVideoExtensions.Contains(extension);
        }
    }
}
