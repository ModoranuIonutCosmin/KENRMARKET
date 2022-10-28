using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStartedDomainEventHandler :
    DomainEventHandler<OrderStartedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderStartedDomainEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public override async Task Handle(OrderStartedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(
            new OrderStartedIntegrationEvent(notification.Order.BuyerId)
        );
    }
}