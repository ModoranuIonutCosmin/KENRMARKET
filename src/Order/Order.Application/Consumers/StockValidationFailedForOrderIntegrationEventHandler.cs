using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Application.Querries;
using Order.Domain.DataModels;

namespace Order.Application.Consumers;

public class
    StockValidationFailedForOrderIntegrationEventHandler : IntegrationEventHandler<
        StockValidationFailedForOrderIntegrationEvent>
{
    private readonly ILogger<StockValidationFailedForOrderIntegrationEventHandler> _logger;
    private readonly IMediator                                                     _mediator;
    private readonly IUnitOfWork                                                   _unitOfWork;

    public StockValidationFailedForOrderIntegrationEventHandler(IMediator mediator,
        ILogger<StockValidationFailedForOrderIntegrationEventHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _mediator   = mediator;
        _logger     = logger;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(StockValidationFailedForOrderIntegrationEvent @event)
    {
        _logger.LogInformation("[Stock validated failure] Received stock validation failed signal for orderId={@orderId}",
                               @event.OrderId);

        var queryOrderByIdCommand = new QueryOrderByIdCommand(@event.OrderId);

        var order = await _mediator.Send(queryOrderByIdCommand);

        order.SetOrderStatus(OrderStatus.StocksValidationAccepted);

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("[Stock validated failure] Changed status to stock validation failed for orderId={@orderId}",
                               @event.OrderId);
    }
}