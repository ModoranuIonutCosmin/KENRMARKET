using Cart.Application.Interfaces.Services;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;

namespace Cart.Application.Consumers;

public class OrderStartedIntegrationEventHandler: IntegrationEventHandler<OrderStartedIntegrationEvent>
{
    private readonly ICartService _cartService;

    public OrderStartedIntegrationEventHandler(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    public override async Task Handle(OrderStartedIntegrationEvent @event)
    {
       await _cartService.DeleteCartContents(@event.CustomerId);
    }
}