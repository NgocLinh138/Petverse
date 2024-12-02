using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.PetService
{
    public static class Query
    {
        public record GetPetServiceQuery : IQuery<PagedResult<Responses.PetServiceResponse>>
        {
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetPetServiceByIdQuery : IQuery<Responses.PetServiceResponse>
        {
            public int Id { get; init; }
        }
    }
}
