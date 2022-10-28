using AutoMapper;
using IntegrationEvents.Base;
using MediatR;
using Order.Application.Commands;

namespace IntegrationEvents.Contracts;

public class CheckoutAcceptedIntegrationEventHandler : IntegrationEventHandler<CheckoutAcceptedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CheckoutAcceptedIntegrationEventHandler(IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    public override async Task Handle(CheckoutAcceptedIntegrationEvent @event)
    {

        CreateNewOrderCommand newOrderCommand = new CreateNewOrderCommand()
        {
            Address = _mapper.Map<Order.Domain.Models.Address>(@event.Address),
            BuyerId = @event.CustomerId,
            CartItems = _mapper.Map<List<Order.Domain.Models.CartItem>>(@event.CartItems)
        };

        await _mediator.Send(newOrderCommand);
    }
}