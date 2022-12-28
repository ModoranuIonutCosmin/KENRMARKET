using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using Order.Application.Interfaces;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStatusChangedToStockValidatedDomainEventHandler :
    DomainEventHandler<OrderStatusChangedToStockValidatedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork      _unitOfWork;

    public OrderStatusChangedToStockValidatedDomainEventHandler(IPublishEndpoint publishEndpoint,
        IUnitOfWork unitOfWork)
    {
        _publishEndpoint = publishEndpoint;
        _unitOfWork      = unitOfWork;
    }


    public override async Task Handle(OrderStatusChangedToStockValidatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var eventToPublish = new OrderStatusChangedToStocksValidatedIntegrationEvent(notification.Order.BuyerId,
         notification.Order.Id,
         (OrderStatus)notification.Order.OrderStatus);

        await _publishEndpoint.Publish(
                                       eventToPublish
                                      );

        await _unitOfWork.CommitTransaction();
    }
}