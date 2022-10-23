using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;

namespace Order.Application.Consumers
{
    public class PaymentSuccessfulForOrderEventHandler: IIntegrationEventHandler<PaymentSuccessfulForOrderEvent>
    {
        private readonly ILogger<PaymentSuccessfulForOrderEventHandler> _logger;
        private readonly IMediator _mediator;

        public PaymentSuccessfulForOrderEventHandler(ILogger<PaymentSuccessfulForOrderEventHandler> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task Handle(PaymentSuccessfulForOrderEvent @event)
        {
            await _mediator.Send(new SetOrderStatusToPaidCommand()
            {
                OrderId = @event.OrderId
            });

            _logger.LogInformation($"Message received, order {@event.OrderId} is now set as paid");
        }
    }
}

