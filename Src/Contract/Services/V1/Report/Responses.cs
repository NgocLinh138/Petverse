using Contract.Enumerations;

namespace Contract.Services.V1.Report;

public static class Responses
{
    public record ReportResponse
    {
        public int Id { get; init; }
        public Guid AppointmentId { get; init; }
        public Guid UserId { get; init; }
        public Guid PetCenterId { get; init; }
        public string Title { get; init; }
        public string Reason { get; init; }
        public ReportStatus Status { get; init; }
        public string CreatedDate { get; init; }
        public string? UpdatedDate { get; init; }
        public List<ReportImageResponse>? ReportImages { get; init; }
    }

    public record ReportImageResponse
    {
        public int Id { get; init; }
        public string URL { get; init; }
        public MediaType Type { get; init; }
    }
}
