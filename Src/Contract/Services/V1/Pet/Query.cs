using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Pet
{
    public static class Query
    {
        public record GetPetQuery : IQuery<PagedResult<Responses.PetResponse>>
        {
            public bool? IncludeDeleted { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; init; }    
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetPetByIdQuery : IQuery<Responses.PetResponse>
        {
            public int Id { get; init; }
        }

        public record GetPetByUserIdQuery : IQuery<PagedResult<Responses.PetResponse>>
        {
            public Guid UserId { get; init; }
            public int PageIndex { get; init; } = 1;
            public int PageSize { get; init; } = 10;
        }
    }
}
