using Contract.Abstractions.Message;
using Contract.Enumerations;

namespace Contract.Services.V1.Job;
public class Command
{
    public record CreateJobCommand(
        Guid PetCenterId,
        string Description,
        bool HavePhoto,
        bool HaveCamera,
        bool HaveTransport,
        IEnumerable<int> SpeciesIds,
        IEnumerable<PetCenterServiceUpdatePrice> PetCenterService) : ICommand<Responses.JobResponse>;

    public record PetCenterServiceUpdatePrice(
        int Id,
        decimal Price,
        int Capacity,
        IEnumerable<ScheduleService> Schedule,
        ServiceType Type);
    public record ScheduleService(
        string Time,
        string Description);

    public record UpdateJobCommand(
        Guid? Id,
        string Description,
        bool HavePhoto,
        bool HaveCamera,
        bool HaveTransport,
        IEnumerable<int>? SpeciesIds) : ICommand<Responses.JobResponse>;

    public record DeleteJobCommand(Guid Id) : ICommand;
}
