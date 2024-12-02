using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;


namespace Application.Usecases.V1.Breed.Queries;
public sealed class GetBreedQueryHandler : IQueryHandler<Query.GetBreedQuery, PagedResult<Responses.BreedResponse>>
{
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;

    public GetBreedQueryHandler(
        IBreedRepository BreedRepository,
        IMapper mapper)
    {
        this.BreedRepository = BreedRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.BreedResponse>>> Handle(Query.GetBreedQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = GetBreedsQuery(request.SearchTerm, request.SpeciesId);

            var Events = await PagedResult<Domain.Entities.Breed>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.BreedResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<Domain.Entities.Breed> GetBreedsQuery(string? searchValue, int? petTypeId)
    {

        var query = string.IsNullOrWhiteSpace(searchValue)
            ? BreedRepository.FindAll()
            : BreedRepository.FindAll(x => x.Name.ToLower().Contains(searchValue.Trim().ToLower()));

        if (petTypeId.HasValue)
            query = query.Where(x => x.SpeciesId == petTypeId);

        return query;
    }
}
