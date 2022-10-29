using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class OrderStatusChangedToPaidIntegrationEventHandler : IntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
{
    private readonly IProductsService _productsService;

    public OrderStatusChangedToPaidIntegrationEventHandler(IProductsService productsService)
    {
        _productsService = productsService;
    }
    
    public override async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
    {
        foreach (var orderProduct in @event.Order.OrderItems)
        {
            await _productsService.DeductStocksForProduct(orderProduct.ProductId, orderProduct.Quantity);
        }
    }
}