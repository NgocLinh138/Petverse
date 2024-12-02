using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.CenterBreed
{
    public static class Query
    {
        public record GetCenterBreedQuery : IQuery<PagedResult<Responses.CenterBreedResponse>>
        {
            public CenterBreedStatus? Status { get; init; }
            public bool? IncludeDisabled { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; init; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetCenterBreedByIdQuery : IQuery<Responses.CenterBreedResponse>
        {
            public int Id { get; init; }
        }

        public record GetCenterBreedByPetCenterIdQuery : IQuery<PagedResult<Responses.CenterBreedResponse>>
        {
            public Guid PetCenterId { get; init; }
            public CenterBreedStatus? Status { get; init; }
            public string? SearchTerm { get; init; }
            public int PageIndex { get; init; } = 1;
            public int PageSize { get; init; } = 10;
        }
    }
}
