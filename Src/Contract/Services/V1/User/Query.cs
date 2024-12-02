using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.User;
public static class Query
{
    public record GetUserQuery : IQuery<PagedResult<Responses.UserGetAllResponse>>
    {
        public Guid? RoleId { get; init; }
        public string? SearchTerm { get; init; }
        public string? SortColumn { get; init; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }

    public record GetPetSitterQuery : IQuery<PagedResult<Responses.PetCenterResponse>>
    {
        public int? ServiceId { get; init; }
    }

    public record GetUserByIdQuery(Guid Id) : IQuery<Responses.UserGetByIdResponse>;
}
