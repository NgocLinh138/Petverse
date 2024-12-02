using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Species.Commands;
public sealed class UpdateSpeciesCommandHandler : ICommandHandler<Command.UpdateSpeciesCommand, Responses.SpeciesResponse>
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IMapper mapper;

    public UpdateSpeciesCommandHandler(
        ISpeciesRepository SpeciesRepository,
        IMapper mapper)
    {
        this.SpeciesRepository = SpeciesRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.SpeciesResponse>> Handle(Command.UpdateSpeciesCommand request, CancellationToken cancellationToken)
    {

        var Species = await SpeciesRepository.FindByIdAsync(request.Id.Value);
        if (Species == null)
            return Result.Failure<Responses.SpeciesResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

        Species.Name = request.Name;
        SpeciesRepository.Update(Species);

        var resultResponse = mapper.Map<Responses.SpeciesResponse>(Species);
        return Result.Success(resultResponse, 202);

    }
}
