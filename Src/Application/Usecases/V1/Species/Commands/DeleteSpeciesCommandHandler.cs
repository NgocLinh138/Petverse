using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Species.Commands;
public sealed class DeleteSpeciesCommandHandler : ICommandHandler<Command.DeleteSpeciesCommand>
{
    private readonly ISpeciesRepository SpeciesRepository;
    public DeleteSpeciesCommandHandler(ISpeciesRepository SpeciesRepository)
    {
        this.SpeciesRepository = SpeciesRepository;
    }

    public async Task<Result> Handle(Command.DeleteSpeciesCommand request, CancellationToken cancellationToken)
    {

        var Species = await SpeciesRepository.FindByIdAsync(request.Id);
        if (Species == null)
            return Result.Failure("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

        SpeciesRepository.Remove(Species);
        return Result.Success(202);

    }
}
