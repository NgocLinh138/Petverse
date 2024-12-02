namespace Contract.Abstractions.Message;
public interface IDomainEvent : MediatR.INotification
{
    public Guid Id { get; init; }
}
