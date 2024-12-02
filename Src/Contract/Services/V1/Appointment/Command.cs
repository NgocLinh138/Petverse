using Contract.Abstractions.Message;
using Contract.Enumerations;

namespace Contract.Services.V1.Appointment;

public static class Command
{
    public record CreateBreedAppointmentCommand(
        Guid UserId,
        int CenterBreedId,
        int PetId,
        decimal Amount,
        string StartTime,
        string EndTime) : ICommand<Responses.BreedAppointmentResponse>;


    public record DeleteAppointmentCommand(Guid Id) : ICommand;

    public record CreateServiceAppointmentCommand(
        Guid UserId,
        int PetId,
        int PetCenterServiceId,
        decimal Amount,
        string StartTime,
        string EndTime,
        ICollection<CreateSchedule> Schedules) : ICommand<Responses.ServiceAppointmentResponse>;
    public record CreateSchedule(string Time, string Description);

    public record UpdateStatusCommand(Guid Id, AppointmentStatus Status, string? CancelReason) : ICommand;
}

