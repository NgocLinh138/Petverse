using Contract.Abstractions.Message;
using Contract.Enumerations;
using Net.payOS.Types;

namespace Contract.Services.V1.Transaction;
public static class Command
{
    public record UpdateTransactionCommand(
       Guid? Id,
       TransactionStatus Status) : ICommand;

    public record CreateAppointmentTransactionCommand(
        Guid UserId,
        Guid AppointmentId,
        string? Title,
        string? Description,
        int Amount,
        List<ItemData> items,
        string returnUrl,
        string cancelUrl) : ICommand<Responses.CreateTransactionResponse>;

    public record CreateAddBalanceTransactionCommand(
        Guid UserId,
        string? Title,
        string? Description,
        int Amount,
        string returnUrl,
        string cancelUrl) : ICommand<Responses.CreateTransactionResponse>;
}
