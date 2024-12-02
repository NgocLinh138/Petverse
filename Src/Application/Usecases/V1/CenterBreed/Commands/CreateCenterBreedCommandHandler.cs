using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories;

namespace Application.Usecases.V1.CenterBreed.Commands
{
    public sealed class CreateCenterBreedCommandHandler : ICommandHandler<Command.CreateCenterBreedCommand, Responses.CenterBreedResponse>
    {
        private readonly ICenterBreedRepository centerBreedRepository;
        private readonly IPetCenterRepository petCenterRepository;
        private readonly ISpeciesRepository speciesRepository;
        private readonly ICenterBreedImageRepository centerBreedImageRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateCenterBreedCommandHandler(
            ICenterBreedRepository centerBreedRepository,
            IPetCenterRepository petCenterRepository,
            ISpeciesRepository speciesRepository,
            ICenterBreedImageRepository centerBreedImageRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.centerBreedRepository = centerBreedRepository;
            this.petCenterRepository = petCenterRepository;
            this.speciesRepository = speciesRepository;
            this.centerBreedImageRepository = centerBreedImageRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.CenterBreedResponse>> Handle(Command.CreateCenterBreedCommand request, CancellationToken cancellationToken)
        {
            var petCenter = await petCenterRepository.FindByIdAsync(request.PetCenterId, cancellationToken);
            if (petCenter == null || petCenter.IsDeleted)
                return Result.Failure<Responses.CenterBreedResponse>("Không tìm thấy trung tâm.", StatusCodes.Status404NotFound);

            var species = await speciesRepository.FindByIdAsync(request.SpeciesId, cancellationToken);
            if (species == null)
                return Result.Failure<Responses.CenterBreedResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

            var existingCenterBreed = await centerBreedRepository.FindSingleAsync(x => x.Name == request.Name && x.PetCenterId == request.PetCenterId, cancellationToken);
            if (existingCenterBreed != null)
                return Result.Failure<Responses.CenterBreedResponse>("Tên của giống thú cưng đã tồn tại trong trung tâm này.", StatusCodes.Status409Conflict);

            try
            {
                var centerBreed = new Domain.Entities.CenterBreed
                {
                    PetCenterId = request.PetCenterId,
                    SpeciesId = request.SpeciesId,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Status = CenterBreedStatus.Processing
                };

                await centerBreedRepository.AddAsync(centerBreed);
                await unitOfWork.SaveChangesAsync();

                List<string> imageUrls = new List<string>();
                if (request.Images != null && request.Images.Count > 0)
                {
                    imageUrls = await AddCenterBreedImages(centerBreed.Id, request.Images);
                }

                var response = mapper.Map<Responses.CenterBreedResponse>(centerBreed);

                return Result.Success(response, 201);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<string>> AddCenterBreedImages(int centerBreedId, ICollection<IFormFile> images)
        {
            List<string> imageUrls = new List<string>();
            Random random = new Random();

            foreach (var image in images)
            {
                try
                {
                    string fileName = $"{centerBreedId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}";
                    string urlString = await blobStorageService.UploadBlob(image, fileName, BlobFolder.centerBreeds);

                    if (string.IsNullOrEmpty(urlString))
                        throw new Exception("Không thể tải hình ảnh lên.");

                    var centerBreedImage = new Domain.Entities.CenterBreedImage
                    {
                        CenterBreedId = centerBreedId,
                        Image = urlString
                    };

                    await centerBreedImageRepository.AddAsync(centerBreedImage);
                    await unitOfWork.SaveChangesAsync();
                    imageUrls.Add(urlString);
                }
                catch (Exception ex)
                {
                    foreach (var url in imageUrls)
                    {
                        await blobStorageService.DeleteBlobSnapshotsAsync(url);
                    }
                    throw new Exception($"Lỗi tải lên hình ảnh: {ex.Message}", ex);
                }
            }
            return imageUrls;
        }
    }
}
