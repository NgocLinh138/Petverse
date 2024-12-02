using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Breed.Commands;
public sealed class DeleteBreedCommandHandler : ICommandHandler<Command.DeleteBreedCommand>
{
    private readonly IBreedRepository BreedRepository;
    public DeleteBreedCommandHandler(IBreedRepository BreedRepository)
    {
        this.BreedRepository = BreedRepository;
    }

    public async Task<Result> Handle(Command.DeleteBreedCommand request, CancellationToken cancellationToken)
    {

        var Breed = await BreedRepository.FindSingleAsync(x => x.Id == request.Id && x.SpeciesId == request.SpeciesId);
        if (Breed == null)
            return Result.Failure("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

        BreedRepository.Remove(Breed);
        return Result.Success(202);
    }
}
