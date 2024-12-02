using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Species.Commands;
public sealed class CreateSpeciesCommandHandler : ICommandHandler<Command.CreateSpeciesCommand, Responses.SpeciesResponse>
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateSpeciesCommandHandler(
        ISpeciesRepository SpeciesRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork)

    {
        this.SpeciesRepository = SpeciesRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<Result<Responses.SpeciesResponse>> Handle(Command.CreateSpeciesCommand request, CancellationToken cancellationToken)
    {

        var newSpecies = new Domain.Entities.Species
        {
            Name = request.Name,
        };

        await SpeciesRepository.AddAsync(newSpecies);
        await unitOfWork.SaveChangesAsync();

        var resultResponse = mapper.Map<Responses.SpeciesResponse>(newSpecies);
        return Result.Success(resultResponse, 201);

    }
}
