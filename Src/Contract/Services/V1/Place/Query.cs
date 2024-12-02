using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.Place
{
    public static class Query
    {
        public record GetPlaceQuery : IQuery<PagedResult<Responses.PlaceResponse>>
        {
            public int? SpeciesId { get; init; }
            public PlaceType? Type { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; set; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetPlaceByIdQuery : IQuery<Responses.PlaceResponse>
        {
            public int Id { get; init; }
        }
    }
}
