using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Consumers;

public class ReservationMadeForItemsEventHandler : IntegrationEventHandler<ReservationMadeForItemsIntegrationEvent>
{
    private readonly IReservationsService                         _reservationsService;
    private readonly ILogger<ReservationMadeForItemsEventHandler> _logger;
    private readonly TimeSpan                                     expiryTime = TimeSpan.FromMinutes(10);

    public ReservationMadeForItemsEventHandler(IReservationsService reservationsService, ILogger<ReservationMadeForItemsEventHandler> logger)
    {
        _reservationsService = reservationsService;
        _logger         = logger;
    }
    
    public override async Task Handle(ReservationMadeForItemsIntegrationEvent @event)
    {
        
        _logger.LogInformation("[Customer reservation] Received reservation message for customerId={@customerId}, orderId={@orderId}", @event.CustomerId, @event.OrderId);

        var reservations = @event.Reservations.Select(r => new ReservationItem(r.ProductId, r.Quantity));

        await _reservationsService.MakeReservationsForCustomer(@event.CustomerId, DateTimeOffset.UtcNow,
                                                               DateTimeOffset.UtcNow.Add(expiryTime),
                                                               false,
                                                               reservations.ToList(),
                                                               @event.OrderId);
        
        _logger.LogInformation("[Customer reservation] Processed reservation message for customerId={@customerId}, orderId={@orderId}", @event.CustomerId, @event.OrderId);
    }
}