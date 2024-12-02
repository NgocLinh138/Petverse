using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Report
{
    public class Command
    {
        public record CreateReportCommand : ICommand<Responses.ReportResponse>
        {
            public Guid AppointmentId { get; init; }
            public string Title { get; init; }
            public string Reason { get; init; }
            public ICollection<IFormFile>? Photos { get; init; }
            public ICollection<IFormFile>? Videos { get; init; }
        }

        public record UpdateReportCommand : ICommand<Responses.ReportResponse>
        {
            public int Id { get; init; }
            public ReportStatus Status { get; init; }
        }

        public record DeleteReportCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
