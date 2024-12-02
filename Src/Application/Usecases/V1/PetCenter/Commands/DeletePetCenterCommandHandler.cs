using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.PetCenter.Commands;
public sealed class DeletePetCenterCommandHandler : ICommandHandler<Command.DeletePetCenterCommand>
{
    private readonly IPetCenterRepository petCenterRepository;
    public DeletePetCenterCommandHandler(IPetCenterRepository petCenterRepository)
    {
        this.petCenterRepository = petCenterRepository;
    }

    public async Task<Result> Handle(Command.DeletePetCenterCommand request, CancellationToken cancellationToken)
    {
        var petCenter = await petCenterRepository.FindByIdAsync(request.Id);
        if (petCenter == null)
            return Result.Failure<Result>("Không tìm thấy trung tâm.", StatusCodes.Status404NotFound);


        petCenterRepository.SoftDelete(petCenter);
        return Result.Success(202);

    }
}
