using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.PetCenter.Commands;
public sealed class UpdatePetCenterCommandHandler : ICommandHandler<Command.UpdatePetCenterCommand>
{
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IBlobStorageService blobStorageService;
    private readonly IApplicationRepository applicationRepository;
    private readonly IMapper mapper;

    public UpdatePetCenterCommandHandler(
        IPetCenterRepository petCenterRepository,
        IMapper mapper,
        IBlobStorageService blobStorageService,
        IApplicationRepository applicationRepository)
    {
        this.petCenterRepository = petCenterRepository;
        this.mapper = mapper;
        this.blobStorageService = blobStorageService;
        this.applicationRepository = applicationRepository;
    }
    public async Task<Result> Handle(Command.UpdatePetCenterCommand request, CancellationToken cancellationToken)
    {
        var PetCenter = await petCenterRepository.FindByIdAsync(request.Id.Value);
        if (PetCenter == null)
            return Result.Failure<Responses.PetCenterResponse>("Không tìm thấy trung tâm.", StatusCodes.Status404NotFound);

        string newBlobPath = string.Empty;
        string oldBlobPath = string.Empty;

        try
        {
            var application = PetCenter.Application;
            // Upload new image if provided
            oldBlobPath = application.Avatar;
            if (application.Avatar != null)
                newBlobPath = await UploadAvatarAsync(request);


            application.Update(request, newBlobPath);
            PetCenter.Update(request);

            return Result.Success(202);
        }
        catch (Exception)
        {
            await blobStorageService.DeleteBlobAsync(newBlobPath);
            throw;
        }
    }

    private async Task<string> UploadAvatarAsync(Command.UpdatePetCenterCommand request)
    {
        var name = $"{request.PhoneNumber}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

        return await blobStorageService.UploadBlob(
            image: request.Image,
            name: name,
            folder: BlobFolder.applications);
    }
}
