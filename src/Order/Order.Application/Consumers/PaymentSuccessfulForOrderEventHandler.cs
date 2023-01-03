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
        _logger.LogInformation("[Payment successful consumer] Setting order status as paid, notifying customer mservice, orderId={@orderId}",
                               @event.OrderId);
        
        await _mediator.Send(new ProcessPaymentSuccessfulCommand()
                             {
                                 OrderId     = @event.OrderId,
                             });

        _logger.LogInformation("[Payment successful consumer] Message sent and now the orderId={@orderId} is set as paid",
                               @event.OrderId);
    }
}