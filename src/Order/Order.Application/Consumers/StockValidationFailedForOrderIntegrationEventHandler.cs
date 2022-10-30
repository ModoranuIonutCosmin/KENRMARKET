using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Order.Application.Interfaces;
using Order.Application.Querries;
using Order.Domain.DataModels;

namespace Order.Application.Consumers;

public class StockValidationFailedForOrderIntegrationEventHandler : IntegrationEventHandler<StockValidationFailedForOrderIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public StockValidationFailedForOrderIntegrationEventHandler(IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(StockValidationFailedForOrderIntegrationEvent @event)
    {
        QueryOrderByIdCommand queryOrderByIdCommand = new QueryOrderByIdCommand(@event.OrderId);

        var order = await _mediator.Send(queryOrderByIdCommand);
        
        order.SetOrderStatus(OrderStatus.StocksValidationAccepted);

        await _unitOfWork.CommitTransaction();
    }
}