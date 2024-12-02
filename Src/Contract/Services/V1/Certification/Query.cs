using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Certification
{
    public static class Query
    {
        public record GetCertificationQuery : IQuery<PagedResult<Responses.CertificationResponse>>
        {
            public int? ApplicationId { get; init; }
            public string? SortColumn { get; init; }
            public int? SortOrder { get; init; }
            public int PageIndex { get; init; }
            public int PageSize { get; init; }
        }

        public record GetCertificationByIdQuery : IQuery<Responses.CertificationResponse>
        {
            public int Id { get; init; }
        }
    }
}
