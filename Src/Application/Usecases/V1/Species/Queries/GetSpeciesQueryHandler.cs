using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;


namespace Application.Usecases.V1.Species.Queries;
public sealed class GetSpeciesQueryHandler : IQueryHandler<Query.GetSpeciesQuery, PagedResult<Responses.SpeciesResponse>>
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IMapper mapper;

    public GetSpeciesQueryHandler(
        ISpeciesRepository SpeciesRepository,
        IMapper mapper)
    {
        this.SpeciesRepository = SpeciesRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.SpeciesResponse>>> Handle(Query.GetSpeciesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var EventsQuery = GetSpeciessQuery(request.SearchTerm);

            var Events = await PagedResult<Domain.Entities.Species>.CreateAsync(
                EventsQuery,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.SpeciesResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<Domain.Entities.Species> GetSpeciessQuery(string? searchValue)
    {

        var query = string.IsNullOrWhiteSpace(searchValue)
            ? SpeciesRepository.FindAll()
            : SpeciesRepository.FindAll(x => x.Name.ToLower().Contains(searchValue.Trim().ToLower()));

        return query;
    }
}
