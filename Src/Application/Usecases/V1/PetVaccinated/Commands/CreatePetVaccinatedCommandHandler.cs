using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.JsonConverters;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.BlobStorage.Services;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetVaccinated.Commands
{
    public sealed class CreatePetVaccinatedCommandHandler : ICommandHandler<Command.CreatePetVaccinatedCommand, Responses.PetVaccinatedResponse>
    {
        private readonly IPetVaccinatedRepository petVaccinatedRepository;
        private readonly IPetRepository petRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreatePetVaccinatedCommandHandler(
            IPetVaccinatedRepository petVaccinatedRepository,
            IPetRepository petRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.petVaccinatedRepository = petVaccinatedRepository;
            this.petRepository = petRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetVaccinatedResponse>> Handle(Command.CreatePetVaccinatedCommand request, CancellationToken cancellationToken)
        {
            var pet = await petRepository.FindByIdAsync(request.PetId, cancellationToken);
            if (pet == null || pet.IsDeleted)
                return Result.Failure<Responses.PetVaccinatedResponse>("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

            string? imageUrl = null;

            try
            {
                if (request.Image != null)
                {
                    imageUrl = await UploadImage(request.Image);
                    if (imageUrl == null) return Result.Failure<Responses.PetVaccinatedResponse>("Hình ảnh không thể để trống.", StatusCodes.Status400BadRequest);
                }

                var petVaccinated = new Domain.Entities.PetVaccinated
                {
                    PetId = request.PetId,
                    Name = request.Name,
                    Image = imageUrl,
                    DateVaccinated = DateTimeConverters.StringToDate(request.DateVaccinated).Value
                };

                await petVaccinatedRepository.AddAsync(petVaccinated);
                await unitOfWork.SaveChangesAsync();

                var response = mapper.Map<Responses.PetVaccinatedResponse>(petVaccinated); 
                return Result.Success(response, 201);
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    await blobStorageService.DeleteBlobSnapshotsAsync(imageUrl);
                }
                throw;
            }
        }

        private async Task<string?> UploadImage(IFormFile? image)
        {
            if (image == null) return null;

            var firstName = image.FileName.Split(' ').FirstOrDefault() ?? "PetVaccinatedImage";
            var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

            return await blobStorageService.UploadBlob(image: image, name: nameImage, folder: BlobFolder.petVaccinateds);
        }

    }
}
