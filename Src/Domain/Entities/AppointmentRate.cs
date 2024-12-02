using Contract.Constants;
using Contract.Services.V1.AppointmentRate;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class AppointmentRate : AuditEntityBase<int>
{
    public Guid AppointmentId { get; set; }
    public float Rate { get; set; }
    public string? Description { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
    public void Update(Command.UpdateAppointmentRateCommand request)
    {
        Rate = request.Rate;
        Description = request.Description;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
