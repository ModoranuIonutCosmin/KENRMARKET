using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStatusChangedToPendingValidationDomainEventHandler :
    DomainEventHandler<OrderStatusChangedToPendingValidationDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderStatusChangedToPendingValidationDomainEventHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public override async Task Handle(OrderStatusChangedToPendingValidationDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var productsAndQuantities = notification.Order.OrderItems
            .Select(oi => new ProductQuantity(oi.ProductId, oi.Quantity));

        var eventToPublish = new OrderPendingStockValidationIntegrationEvent(notification.Order.BuyerId,
            notification.Order.Id,
            productsAndQuantities.ToList(),
            (OrderStatus)notification.Order.OrderStatus);

        await _publishEndpoint.Publish(
            eventToPublish
        );
    }
}