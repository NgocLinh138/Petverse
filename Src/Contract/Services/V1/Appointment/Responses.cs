using Contract.Enumerations;
using static Contract.Services.V1.AppointmentRate.Responses;
using static Contract.Services.V1.Report.Responses;

namespace Contract.Services.V1.Appointment;

public static class Responses
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string CenterName { get; set; }
        public Guid PetCenterId { get; set; }
        public int PetId { get; set; }
        public AppointmentType Type { get; set; }
        public bool IsReported { get; set; }
        public string? CancelReason { get; set; }
        public decimal Amount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public AppointmentStatus Status { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
    }

    public class AppointmentByIdResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string CenterName { get; set; }
        public Guid PetCenterId { get; set; }
        public int SpeciesId { get; set; }
        public AppointmentType Type { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsReported { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? CancelReason { get; set; }
        public string CreatedDate { get; set; } = string.Empty;
        public string? UpdatedDate { get; set; } = string.Empty;
        public int? PetCenterServiceId { get; set; }
        public int? CenterBreedId { get; set; }

        public ReportResponse Report { get; set; }
        public AppointmentRateResponse Rate { get; set; }
        public PetResponse Pet { get; set; }
        public IEnumerable<ScheduleAppointmentResponse> Schedules { get; set; }
    }

    public record PetResponse(
        int Id,
        int BreedId,
        string Name,
        string BirthDate,
        Gender Gender,
        float Weight,
        bool Sterilized,
        string Avatar,
        string? Description);

    public record ScheduleAppointmentResponse(
        string Date,
        IEnumerable<Record> Records);

    public record Record(
        int ScheduleId,
        string Time,
        string Description,
        IEnumerable<Schedule.Responses.TrackingResponse> Trackings);


    public record BreedAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int CenterBreedId { get; set; }
        public int PetId { get; set; }
        public decimal Amount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
    }

    public record ServiceAppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int PetId { get; set; }
        public int PetCenterServiceId { get; set; }
        public decimal Amount { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
    }

    public record BreedAppointmetByUserResponse(ICollection<BreedAppointemntData> breedAppointemnts);

    public record BreedAppointemntData(
        Guid AppointemntId,
        int PetId,
        int CenterBreedId,
        string Date);
}

