using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Domain.DataModels;

namespace Order.Application.Consumers;

public class PaymentSuccessfulForOrderEventHandler : IntegrationEventHandler<OrderPaymentSuccessfulIntegrationEvent>
{
    private readonly ILogger<PaymentSuccessfulForOrderEventHandler> _logger;
    private readonly IMediator                                      _mediator;

    public PaymentSuccessfulForOrderEventHandler(ILogger<PaymentSuccessfulForOrderEventHandler> logger,
        IMediator mediator)
    {
        _logger   = logger;
        _mediator = mediator;
    }

    public override async Task Handle(OrderPaymentSuccessfulIntegrationEvent @event)
    {
        _logger.LogInformation("[Payment successful consumer] Setting order status as paid, orderId={@event.OrderId} is now set as paid",
                               @event.OrderId);

        await _mediator.Send(new SetOrderStatusCommand
                             {
                                 OrderId     = @event.OrderId,
                                 OrderStatus = OrderStatus.Paid
                             });

        _logger.LogInformation("[Payment successful consumer] Message received, orderId={@event.OrderId} is now set as paid",
                               @event.OrderId);
    }
}