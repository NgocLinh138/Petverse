using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.Application
{
    public static class Query
    {
        public record GetApplicationQuery : IQuery<PagedResult<Responses.ApplicationResponse>>
        {
            public Guid? UserId { get; set; }
            public JobApplicationStatus? Status { get; init; }
            public string? SearchTerm { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; set; }
            public int PageIndex { get; init; } 
            public int PageSize { get; init; }
        }

        public record GetApplicationByIdQuery : IQuery<Responses.ApplicationResponse>
        {
            public int Id { get; init; }
        }

        public record GetApplicationStatusByUserIdQuery : IQuery<Responses.ApplicationResponse>
        {
            public Guid UserId { get; init; }
        }
    }
}
