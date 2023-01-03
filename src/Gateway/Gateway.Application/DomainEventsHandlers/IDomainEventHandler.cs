using Gateway.Domain.Shared;
using MediatR;

namespace Gateway.Application.DomainEventsHandlers;

public interface IDomainEventHandler<TNotification> : INotificationHandler<TNotification>
    where TNotification : IDomainEvent
{
    public Task Handle(TNotification notification, CancellationToken cancellationToken);
}