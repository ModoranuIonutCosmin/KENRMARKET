using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.AspNetCore.SignalR;
using Order.SignalR.Hubs;

namespace Order.SignalR.Consumers;

public class OrderStatusChangedToPendingStockValidationIntegrationEventHandler : IntegrationEventHandler<
    OrderStatusChangedToPendingStockValidationIntegrationEvent>
{
    private readonly IHubContext<OrdersHub> _hubContext;

    public OrderStatusChangedToPendingStockValidationIntegrationEventHandler(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task Handle(OrderStatusChangedToPendingStockValidationIntegrationEvent @event)
    {
        await _hubContext.Clients.User(@event.CustomerId.ToString())
                         .SendAsync("ReceiveOrdersUpdate", new
                                                           {
                                                               @event.OrderId, @event.OrderStatus
                                                           });
    }
}