using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Breed.Queries;
public sealed class GetBreedByIdQueryHandler : IQueryHandler<Query.GetBreedByIdQuery, Responses.BreedResponse>
{
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;

    public GetBreedByIdQueryHandler(
         IBreedRepository BreedRepository,
        IMapper mapper)
    {
        this.BreedRepository = BreedRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.BreedResponse>> Handle(Query.GetBreedByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await BreedRepository.FindSingleAsync(x => x.Id == request.Id && x.SpeciesId == request.SpeciesId);
        if (result == null)
            return Result.Failure<Responses.BreedResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

        var resultResponse = mapper.Map<Responses.BreedResponse>(result);
        return Result.Success(resultResponse);
    }
}
