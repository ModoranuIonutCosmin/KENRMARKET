using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.AspNetCore.SignalR;
using Order.SignalR.Hubs;

namespace Order.SignalR.Consumers;

public class
    OrderStatusChangedToPaidIntegrationEventHandler : IntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
{
    private readonly IHubContext<OrdersHub> _hubContext;

    public OrderStatusChangedToPaidIntegrationEventHandler(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
    {
        await _hubContext.Clients.User(@event.CustomerId.ToString())
                         .SendAsync("ReceiveOrdersUpdate", new
                                                           {
                                                               @event.OrderId, @event.OrderStatus
                                                           });
    }
}