using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;

namespace Contract.Services.V1.Report;

public static class Query
{
    public record GetReportQuery : IQuery<PagedResult<Responses.ReportResponse>>
    {
        public Guid? AppointmentId { get; init; }
        public Guid? UserId { get; init; }
        public Guid? PetCenterId { get; init; }
        public string? SearchTerm { get; init; }
        public string? SortColumn { get; init; }
        public int? SortOrder { get; init; }
        public int PageIndex { get; init; }
        public int PageSize { get; init; }
    }

    public record GetReportByIdQuery : IQuery<Responses.ReportResponse>
    {
        public int Id { get; init; }
    }
}
