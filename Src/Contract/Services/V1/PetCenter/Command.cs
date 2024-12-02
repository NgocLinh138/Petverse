using Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.PetCenter;
public class Command
{
    public record CreatePetCenterCommand(
        Guid UserId,
        string Name,
        string Address,
        string PhoneNumber,
        bool IsVerified,
        int Yoe,
        IEnumerable<int> ListServiceIds) : ICommand<Responses.PetCenterResponse>;

    public record UpdatePetCenterCommand(
        Guid? Id,
        string? Name,
        IFormFile? Image,
        string? Address,
        string? PhoneNumber,
        string? Description,
        string? BankName,
        string? BankNumber) : ICommand;

    public record DeletePetCenterCommand(Guid Id) : ICommand;
}
