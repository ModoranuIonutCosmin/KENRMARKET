using MediatR;
using Order.Domain.Shared;

namespace Order.Application.DomainEventsHandlers;

public abstract class DomainEventHandler<TNotification> : IDomainEventHandler<TNotification>
    where TNotification : IDomainEvent
{
    async Task INotificationHandler<TNotification>.Handle(TNotification notification, CancellationToken cancellationToken)
    {
        await (this as IDomainEventHandler<TNotification>).Handle(notification, cancellationToken);
    }

    public abstract Task Handle(TNotification notification, CancellationToken cancellationToken);
}