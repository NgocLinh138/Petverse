using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Breed.Commands;
public sealed class UpdateBreedCommandHandler : ICommandHandler<Command.UpdateBreedCommand, Responses.BreedResponse>
{
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;

    public UpdateBreedCommandHandler(
        IBreedRepository BreedRepository,
        IMapper mapper)
    {
        this.BreedRepository = BreedRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.BreedResponse>> Handle(Command.UpdateBreedCommand request, CancellationToken cancellationToken)
    {

        var Breed = await BreedRepository.FindSingleAsync(x => x.Id == request.Id && x.SpeciesId == request.SpeciesId);
        if (Breed == null)
            return Result.Failure<Responses.BreedResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

        Breed.SpeciesId = request.SpeciesId.Value;
        Breed.Name = request.Name;
        Breed.Description = request.Description;
        BreedRepository.Update(Breed);

        var resultResponse = mapper.Map<Responses.BreedResponse>(Breed);
        return Result.Success(resultResponse, 202);

    }
}
