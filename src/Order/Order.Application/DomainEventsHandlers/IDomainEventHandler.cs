using MediatR;
using Order.Domain.Shared;

namespace Order.Application.DomainEventsHandlers;

public interface IDomainEventHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : IDomainEvent
{
    public Task Handle(TNotification @notification, CancellationToken cancellationToken);
}