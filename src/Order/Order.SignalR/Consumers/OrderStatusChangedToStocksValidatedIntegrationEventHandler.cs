using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.AspNetCore.SignalR;
using Order.SignalR.Hubs;

namespace Order.SignalR.Consumers;

public class OrderStatusChangedToStocksValidatedIntegrationEventHandler : IntegrationEventHandler<OrderStatusChangedToStocksValidatedIntegrationEvent>
{
    
    private readonly IHubContext<OrdersHub> _hubContext;

    public OrderStatusChangedToStocksValidatedIntegrationEventHandler(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public override async Task Handle(OrderStatusChangedToStocksValidatedIntegrationEvent @event)
    {
        await _hubContext.Clients.User(@event.CustomerId.ToString())
            .SendAsync("ReceiveOrdersUpdate", new
            {
                OrderId = @event.OrderId,
                OrderStatus = @event.OrderStatus
            });
    }
}