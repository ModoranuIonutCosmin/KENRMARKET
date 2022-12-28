using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class OrderStatusChangedToPendingStockValidationIntegrationEventHandler : IntegrationEventHandler<
    OrderStatusChangedToPendingStockValidationIntegrationEvent>
{
    private readonly ILogger<OrderStatusChangedToPendingStockValidationIntegrationEventHandler> _logger;
    private readonly IProductsService                                                           _productsService;
    private readonly IPublishEndpoint                                                           _publishEndpoint;

    public OrderStatusChangedToPendingStockValidationIntegrationEventHandler(IProductsService productsService,
        IPublishEndpoint publishEndpoint,
        ILogger<OrderStatusChangedToPendingStockValidationIntegrationEventHandler> logger)
    {
        _productsService = productsService;
        _publishEndpoint = publishEndpoint;
        _logger          = logger;
    }

    public override async Task Handle(OrderStatusChangedToPendingStockValidationIntegrationEvent @event)
    {
        _logger.LogInformation("Checking if enough products are available for {@orderId}", @event.OrderId);
        if (await _productsService.AreProductsOnStock(@event.OrderProducts))
        {
            _logger.LogInformation("Stock validated for orderId={@orderId}", @event.OrderId);
            await _publishEndpoint.Publish(
                                           new StockValidatedForOrderIntegrationEvent(@event.CustomerId, @event.OrderId,
                                            @event.OrderStatus)
                                          );

            _logger.LogInformation("Published stock successful event for orderId={@orderId}", @event.OrderId);
        }
        else
        {
            _logger.LogInformation("Stock validation failed for orderId={@orderId}", @event.OrderId);

            await _publishEndpoint.Publish(
                                           new StockValidationFailedForOrderIntegrationEvent(@event.CustomerId,
                                            @event.OrderId,
                                            @event.OrderStatus)
                                          );

            _logger.LogInformation("Published stock failed event for orderId={@orderId}", @event.OrderId);
        }
    }
}