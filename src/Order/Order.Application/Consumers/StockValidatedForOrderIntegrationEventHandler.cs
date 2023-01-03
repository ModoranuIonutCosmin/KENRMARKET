using IntegrationEvents.Base;
using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Application.Querries;
using OrderStatus = Order.Domain.DataModels.OrderStatus;

namespace Order.Application.Consumers;

public class
    StockValidatedForOrderIntegrationEventHandler : IntegrationEventHandler<StockValidatedForOrderIntegrationEvent>
{
    private readonly ILogger<StockValidatedForOrderIntegrationEventHandler> _logger;
    private readonly IPublishEndpoint publishEndpoint;
    private readonly IMediator                                              _mediator;
    private readonly IUnitOfWork                                            _unitOfWork;

    public StockValidatedForOrderIntegrationEventHandler(IMediator mediator,
        ILogger<StockValidatedForOrderIntegrationEventHandler> logger,
        IPublishEndpoint publishEndpoint,
        IUnitOfWork unitOfWork)
    {
        _mediator   = mediator;
        _logger     = logger;
        this.publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(StockValidatedForOrderIntegrationEvent @event)
    {
        _logger.LogInformation("[Stock validated success] Received stock validated signal for orderId={@orderId}",
                               @event.OrderId);

        var queryOrderByIdCommand = new QueryOrderByIdCommand(@event.OrderId);

        var order = await _mediator.Send(queryOrderByIdCommand);

        var reservationItems = order.OrderItems
                                    .Select(oi => new ProductQuantity(oi.ProductId, oi.Quantity))
                                    .ToList();


        await publishEndpoint.Publish(new ReservationMadeForItemsIntegrationEvent(reservationItems,
                                           order.BuyerId, order.Id
                                          ));

        order.SetOrderStatus(OrderStatus.StocksValidationAccepted);

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("[Stock validated success] Changed status to stock validated for orderId={@orderId}",
                               @event.OrderId);
    }
}