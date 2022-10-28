
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class OrderPendingStockValidationIntegrationEventHandler : IntegrationEventHandler<OrderPendingStockValidationIntegrationEvent>
{
    private readonly IProductsService _productsService;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderPendingStockValidationIntegrationEventHandler(IProductsService productsService,
        IPublishEndpoint publishEndpoint)
    {
        _productsService = productsService;
        _publishEndpoint = publishEndpoint;
    }
    
    public override async Task Handle(OrderPendingStockValidationIntegrationEvent @event)
    {
        IIntegrationEvent validationCheckResultIntegrationEvent =
            await _productsService.AreProductsOnStock(@event.OrderProducts)
                ? new StockValidatedForOrderIntegrationEvent(@event.CustomerId, @event.OrderId, @event.OrderStatus)
                : new StockValidationFailedForOrderIntegrationEvent(@event.CustomerId, @event.OrderId, @event.OrderStatus);

        await _publishEndpoint.Publish(validationCheckResultIntegrationEvent);
    }
}