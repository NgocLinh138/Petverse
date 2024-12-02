using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.Role;
public static class Query
{
    public record GetRoleQuery : IQuery<PagedResult<Responses.RoleResponse>>
    {
        public string? SearchTerm { get; init; }
        public string? SortColumn { get; init; }
        public SortOrder? SortOrder { get; init; }
        public int PageIndex { get; init; }
        public int PageSize { get; init; }

    }

    public record GetRoleByIdQuery : IQuery<Responses.RoleResponse>
    {
        public Guid Id { get; init; }
    }

}
