
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class OrderStatusChangedToPendingStockValidationIntegrationEventHandler : IntegrationEventHandler<OrderStatusChangedToPendingStockValidationIntegrationEvent>
{
    private readonly IProductsService _productsService;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderStatusChangedToPendingStockValidationIntegrationEventHandler(IProductsService productsService,
        IPublishEndpoint publishEndpoint)
    {
        _productsService = productsService;
        _publishEndpoint = publishEndpoint;
    }
    
    public override async Task Handle(OrderStatusChangedToPendingStockValidationIntegrationEvent @event)
    {
        if (await _productsService.AreProductsOnStock(@event.OrderProducts))
        {
            await _publishEndpoint.Publish(
                    new StockValidatedForOrderIntegrationEvent(@event.CustomerId, @event.OrderId, @event.OrderStatus)
                );
        }
        else
        {
            await _publishEndpoint.Publish(
                new StockValidationFailedForOrderIntegrationEvent(@event.CustomerId, @event.OrderId,
                    @event.OrderStatus)
            );
        }
    }
}