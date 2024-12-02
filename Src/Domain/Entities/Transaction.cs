using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Transaction;
using Domain.Abstractions.EntityBase;
using Domain.Entities.Identity;

namespace Domain.Entities;

public class Transaction : EntityBase<Guid>, IDateTracking
{
    public Guid? AppointmentId { get; set; }
    public Guid UserId { get; set; }
    public int OrderCode { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool IsMinus { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    public virtual AppUser User { get; set; } = null!;
    public virtual Appointment? Appointment { get; set; } = null!;

    public static Transaction CreateAppointmentTransaction(Command.CreateAppointmentTransactionCommand request, int orderCode)
        => new Transaction
        {
            OrderCode = orderCode,
            AppointmentId = request.AppointmentId,
            IsMinus = true,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,

        };

    public static Transaction CreateAddBalanceTransaction(Command.CreateAddBalanceTransactionCommand request, int orderCode)
        => new Transaction
        {
            OrderCode = orderCode,
            IsMinus = false,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Amount = request.Amount,
        };
}
