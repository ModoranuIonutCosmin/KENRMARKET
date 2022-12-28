using AutoMapper;
using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Commands;
using Order.Domain.Models;

namespace Order.Application.Consumers;

public class CheckoutAcceptedIntegrationEventHandler : IntegrationEventHandler<CheckoutAcceptedIntegrationEvent>
{
    private readonly ILogger<CheckoutAcceptedIntegrationEventHandler> _logger;
    private readonly IMapper                                          _mapper;
    private readonly IMediator                                        _mediator;

    public CheckoutAcceptedIntegrationEventHandler(IMediator mediator,
        ILogger<CheckoutAcceptedIntegrationEventHandler> logger,
        IMapper mapper)
    {
        _mediator = mediator;
        _logger   = logger;
        _mapper   = mapper;
    }

    public override async Task Handle(CheckoutAcceptedIntegrationEvent @event)
    {
        _logger.LogInformation("[Checkout accepted] Message received for buyerId={@buyerId}, orderId={@orderId}",
                               @event.CustomerId,
                               @event.Id);

        var newOrderCommand = new CreateNewOrderCommand
                              {
                                  Address   = _mapper.Map<Address>(@event.Address),
                                  BuyerId   = @event.CustomerId,
                                  CartItems = _mapper.Map<List<CartItem>>(@event.CartItems)
                              };

        await _mediator.Send(newOrderCommand);

        _logger.LogInformation("[Checkout accepted] Processing - create order - done for buyerId={@buyerId}, orderId={@orderId}",
                               @event.CustomerId,
                               @event.Id);
    }
}