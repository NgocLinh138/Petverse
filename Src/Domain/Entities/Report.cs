using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Report;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class Report : EntityBase<int>, IDateTracking
{
    public Guid AppointmentId { get; set; }
    public string Title { get; set; } = null!;
    public string? Reason { get; set; } = null!;
    public ReportStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
    public virtual ICollection<ReportImage> ReportImages { get; set; }


    public void Update(Command.UpdateReportCommand request)
    {
        Status = request.Status;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
