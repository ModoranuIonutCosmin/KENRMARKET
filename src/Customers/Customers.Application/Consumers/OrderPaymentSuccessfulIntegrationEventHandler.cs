using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Consumers;

public class OrderPaymentSuccessfulIntegrationEventHandler : IntegrationEventHandler<OrderPaymentSuccessfulIntegrationEvent>
{
    private readonly IReservationsService                                   _reservationsService;
    private readonly ILogger<OrderPaymentSuccessfulIntegrationEventHandler> _logger;

    public OrderPaymentSuccessfulIntegrationEventHandler(IReservationsService reservationsService,
        ILogger<OrderPaymentSuccessfulIntegrationEventHandler> logger)
    {
        _reservationsService = reservationsService;
        _logger              = logger;
    }

    public override async Task Handle(OrderPaymentSuccessfulIntegrationEvent @event)
    {
        _logger.LogInformation("[Customer reservation] Received clear reservations message for customerId={@customerId}",
                               @event.PayerId);

        await _reservationsService.RemoveReservationByCustomerId(@event.PayerId);

        _logger.LogInformation("[Customer reservation] Processed  clear reservations message  for customerId={@customerId}",
                               @event.PayerId);
    }
}