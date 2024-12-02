using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Species;
public static class Query
{
    public record GetSpeciesQuery : IQuery<PagedResult<Responses.SpeciesResponse>>
    {
        public string? SearchTerm { get; init; }
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }

    public record GetSpeciesByIdQuery : IQuery<Responses.SpeciesResponse>
    {
        public int Id { get; init; }
    }

    public record GetBreedQuery : IQuery<PagedResult<Responses.BreedResponse>>
    {
        public int? SpeciesId { get; init; }
        public string? SearchTerm { get; init; }
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }

    public record GetBreedByIdQuery : IQuery<Responses.BreedResponse>
    {
        public int SpeciesId { get; init; }
        public int Id { get; init; }
    }

}
