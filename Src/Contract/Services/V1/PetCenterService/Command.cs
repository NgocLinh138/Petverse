using Contract.Abstractions.Message;
using Contract.Enumerations;
using static Contract.Services.V1.PetCenterService.Responses;

namespace Contract.Services.V1.PetCenterService;

public static class Command
{
    public record CreatePetCenterServiceCommand(
        Guid PetCenterId,
        int PetServiceId,
        decimal Price,
        string? Description,
        ServiceType Type) : ICommand<Responses.PetCenterServiceResponse>;

    public record UpdatePetCenterServiceCommand(
        int Id,
        decimal? Price,
        string? Description,
        ServiceType? Type,
        IEnumerable<ScheduleService>? Schedule) : ICommand<Responses.PetCenterServiceResponse>;

    public record DeletePetCenterServiceCommand(int Id) : ICommand;
}

