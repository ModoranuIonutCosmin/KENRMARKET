using Cart.Application.Interfaces.Services;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using Microsoft.Extensions.Logging;

namespace Cart.Application.Consumers;

public class OrderStartedIntegrationEventHandler : IntegrationEventHandler<OrderStartedIntegrationEvent>
{
    private readonly ICartService                                 _cartService;
    private readonly ILogger<OrderStartedIntegrationEventHandler> _logger;

    public OrderStartedIntegrationEventHandler(ICartService cartService,
        ILogger<OrderStartedIntegrationEventHandler> logger)
    {
        _cartService = cartService;
        _logger      = logger;
    }

    public override async Task Handle(OrderStartedIntegrationEvent @event)
    {
        _logger.LogInformation("[Order started] Deleting customer cart");
        await _cartService.DeleteCartContents(@event.CustomerId);
        _logger.LogInformation("[Order started] Deleted customer cart successfully");
    }
}