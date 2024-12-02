using Contract.Constants;
using Contract.Enumerations;
using Domain.Abstractions.EntityBase;
using Domain.Entities.Identity;

namespace Domain.Entities;

public abstract class Appointment : EntityBase<Guid>, IDateTracking
{
    public Guid UserId { get; set; }
    public int PetId { get; set; }
    public Guid PetCenterId { get; set; }
    public AppointmentType Type { get; set; }
    public bool IsReported { get; set; } = false;
    public decimal Amount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentStatus Status { get; set; }
    public string? CancelReason { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public virtual AppUser User { get; set; } = null!;
    public virtual Pet Pet { get; set; } = null!;
    public virtual PetCenter PetCenter { get; set; } = null!;
    public virtual Report Report { get; set; } = null!;
    public virtual AppointmentRate AppointmentRate { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = null!;
}
