using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.PetVaccinated;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetVaccinated.Commands
{
    public sealed class UpdatePetVaccinatedCommandHandler : ICommandHandler<Command.UpdatePetVaccinatedCommand, Responses.PetVaccinatedResponse>
    {
        private readonly IPetVaccinatedRepository petVaccinatedRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePetVaccinatedCommandHandler(
            IPetVaccinatedRepository petVaccinatedRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.petVaccinatedRepository = petVaccinatedRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.PetVaccinatedResponse>> Handle(Command.UpdatePetVaccinatedCommand request, CancellationToken cancellationToken)
        {
            var petVaccinated = await petVaccinatedRepository.FindByIdAsync(request.Id, cancellationToken);
            if (petVaccinated == null)
                return Result.Failure<Responses.PetVaccinatedResponse>("Không tìm thấy thông tin đã tiêm phòng của thú cưng.", StatusCodes.Status404NotFound);

            string? imageUrl = null;

            try
            {
                petVaccinated.Update(request);

                if (request.Image != null)
                {
                    var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
                    var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

                    imageUrl = await blobStorageService.UploadBlob(image: request.Image, name: nameImage, folder: BlobFolder.petVaccinateds)
                                ?? throw new Exception("Không thể tải tệp lên.");

                    petVaccinated.Image = imageUrl;
                }

                petVaccinatedRepository.Update(petVaccinated);
                await unitOfWork.SaveChangesAsync();

                var response = mapper.Map<Responses.PetVaccinatedResponse>(petVaccinated);

                return Result.Success(response, 202);
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
    }
}
