using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class Schedule : EntityBase<int>
{
    public Guid ServiceAppointmentId { get; set; }
    public DateOnly Date { get; set; }
    public string Time { get; set; }
    public string Description { get; set; }

    public virtual ServiceAppointment ServiceAppointment { get; set; } = null!;
    public virtual ICollection<Tracking> Trackings { get; set; }
}
