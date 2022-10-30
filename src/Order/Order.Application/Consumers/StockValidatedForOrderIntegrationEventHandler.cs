using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Order.Application.Interfaces;
using Order.Application.Querries;
using Order.Domain.DataModels;

namespace Order.Application.Consumers;

public class StockValidatedForOrderIntegrationEventHandler : IntegrationEventHandler<StockValidatedForOrderIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public StockValidatedForOrderIntegrationEventHandler(IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(StockValidatedForOrderIntegrationEvent @event)
    {
        QueryOrderByIdCommand queryOrderByIdCommand = new QueryOrderByIdCommand(@event.OrderId);

        var order = await _mediator.Send(queryOrderByIdCommand);
        
        order.SetOrderStatus(OrderStatus.StocksValidationAccepted);

        await _unitOfWork.CommitTransaction();
    }
}