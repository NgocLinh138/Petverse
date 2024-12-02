namespace Domain.Entities;

public class ServiceAppointment : Appointment
{
    public int PetCenterServiceId { get; set; }
    public virtual PetCenterService PetCenterService { get; set; } = null!;
    public virtual ICollection<Schedule> Schedules { get; set; } = null!;
}
