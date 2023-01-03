using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces.Services;

namespace Products.Application.Consumers;

public class ReservationMadeForItemsEventHandler : IntegrationEventHandler<ReservationMadeForItemsIntegrationEvent>
{
    private readonly IPublishEndpoint                             _publishEndpoint;
    private readonly IProductsService                             _productsService;
    private readonly ILogger<ReservationMadeForItemsEventHandler> _logger;

    public ReservationMadeForItemsEventHandler(IPublishEndpoint publishEndpoint,
        IProductsService productsService,
        ILogger<ReservationMadeForItemsEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _productsService = productsService;
        _logger     = logger;
    }
    
    public override async Task Handle(ReservationMadeForItemsIntegrationEvent @event)
    {
        _logger.LogInformation("[Reservation creating] Attempting to deduct stock for orderId={@orderId}, customerId={@customerId}", @event.OrderId, @event.CustomerId);
        var reservations = @event.Reservations.Select(r => (r.ProductId, r.Quantity)).ToList();
        await _productsService.DeductStocksForProducts(reservations);
        _logger.LogInformation("[Reservation creating] Successful reservation for orderId={@orderId}, customerId={@customerId}", @event.OrderId, @event.CustomerId);
    }
}