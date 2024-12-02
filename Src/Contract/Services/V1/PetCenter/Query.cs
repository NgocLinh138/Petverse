using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.PetCenter;
public static class Query
{
    public record GetPetCenterQuery : IQuery<PagedResult<Responses.PetCenterGetAllResponse>>
    {
        public int? PetServiceId { get; set; }
        public string? SearchTerm { get; init; }
        public string? SortColumn { get; init; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public record GetPetCenterByIdQuery(Guid Id) : IQuery<Responses.PetCenterGetByIdResponse>;

    public record GetTop5PetCenterQuery(int Month, int Year) : IQuery<Responses.TopPetCenterResponse>;
}
