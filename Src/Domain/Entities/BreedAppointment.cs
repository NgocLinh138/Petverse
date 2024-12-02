namespace Domain.Entities;

public class BreedAppointment : Appointment
{
    public int CenterBreedId { get; set; }
    public virtual CenterBreed CenterBreed { get; set; }
}