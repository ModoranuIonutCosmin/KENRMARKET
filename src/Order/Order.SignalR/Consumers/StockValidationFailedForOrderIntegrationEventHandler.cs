using IntegrationEvents.Base;
using IntegrationEvents.Models;
using Microsoft.AspNetCore.SignalR;
using Order.SignalR.Hubs;

namespace IntegrationEvents.Contracts;

public class StockValidationFailedForOrderIntegrationEventHandler : IntegrationEventHandler<StockValidationFailedForOrderIntegrationEvent>
{
    private readonly IHubContext<OrdersHub> _hubContext;

    public StockValidationFailedForOrderIntegrationEventHandler(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task Handle(StockValidationFailedForOrderIntegrationEvent @event)
    {
        await _hubContext.Clients.User(@event.CustomerId.ToString())
            .SendAsync("ReceiveOrdersUpdate", new
            {
                OrderId = @event.OrderId,
                OrderStatus = @event.OrderStatus
            });
    }
}