using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.AspNetCore.SignalR;
using Order.SignalR.Hubs;

namespace Order.SignalR.Consumers;

public class OrderStatusChangedToStocksValidationRejectedIntegrationEventHandler : IntegrationEventHandler<
    OrderStatusChangedToStocksValidationRejectedIntegrationEvent>
{
    private readonly IHubContext<OrdersHub> _hubContext;

    public OrderStatusChangedToStocksValidationRejectedIntegrationEventHandler(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task Handle(OrderStatusChangedToStocksValidationRejectedIntegrationEvent @event)
    {
        await _hubContext.Clients.User(@event.CustomerId.ToString())
                         .SendAsync("ReceiveOrdersUpdate", new
                                                           {
                                                               @event.OrderId, @event.OrderStatus
                                                           });
    }
}