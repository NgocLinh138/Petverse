using MediatR;

namespace Contract.Abstractions.Message;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : IDomainEvent
{
}
