using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;

namespace Order.Application.Consumers;

public class ReservationExpiredIntegrationEventHandler : IntegrationEventHandler<
    ReservationExpiredIntegrationEvent>
{
    private readonly ILogger<ReservationExpiredIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public ReservationExpiredIntegrationEventHandler(
        ILogger<ReservationExpiredIntegrationEventHandler> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    public override async Task Handle(ReservationExpiredIntegrationEvent @event)
    {
        _logger.LogInformation("[Reservation expired message in orders] Cancelling orderId={@orderId} for customerId={@customerId}.", @event.OrderId, @event.CustomerId);

        //TODO: Cancel payment link

        await _mediator.Send(new SetOrderStatusCommand
        {
            OrderId = @event.OrderId,
            OrderStatus = Domain.DataModels.OrderStatus.Expired
        });

        _logger.LogInformation("[Reservation expired message in orders] Cancelled orderId={@orderId} for customerId={@customerId}.", @event.OrderId, @event.CustomerId);
    }
}