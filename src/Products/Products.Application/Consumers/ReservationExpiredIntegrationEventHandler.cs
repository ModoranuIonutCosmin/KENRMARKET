using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class ReservationExpiredIntegrationEventHandler :IntegrationEventHandler<
    ReservationExpiredIntegrationEvent>
{
    private readonly IProductsService                             _productsService;
    private readonly ILogger<ReservationMadeForItemsEventHandler> _logger;

    public ReservationExpiredIntegrationEventHandler(        IProductsService productsService,
        ILogger<ReservationMadeForItemsEventHandler> logger)
    {
        _productsService = productsService;
        _logger     = logger;
    }
    public override async Task Handle(ReservationExpiredIntegrationEvent @event)
    {
        _logger.LogInformation("[Reservation expired message in products] Replenishing stock for products from cancelled reservation for customerId={@customerId}.", @event.CustomerId);
        
        await _productsService.AddStocksForProducts(@event.Reservations);
        
        _logger.LogInformation("[Reservation expired message in products] Successfully replenished stock for products from cancelled reservation for customerId={@customerId}.", @event.CustomerId);
    }
}