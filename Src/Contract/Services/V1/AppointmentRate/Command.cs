using Contract.Abstractions.Message;

namespace Contract.Services.V1.AppointmentRate
{
    public class Command
    {
        public record CreateAppointmentRateCommand : ICommand<Responses.AppointmentRateResponse>
        {
            public Guid AppointmentId { get; init; }
            public float Rate { get; init; }
            public string? Description { get; init; }
        }

        public record UpdateAppointmentRateCommand : ICommand<Responses.AppointmentRateResponse>
        {
            public int Id { get; init; }
            public float Rate { get; init; }
            public string? Description { get; init; }
        }

        public record DeleteAppointmentRateCommand : ICommand
        {
            public int Id { get; init; }
        }
    }
}
