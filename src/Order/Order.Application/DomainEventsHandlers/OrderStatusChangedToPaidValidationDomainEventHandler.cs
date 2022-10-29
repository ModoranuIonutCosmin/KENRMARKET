using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using Order.Application.Interfaces;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStatusChangedToPaidValidationDomainEventHandler :
    DomainEventHandler<OrderStatusChangedToPaidValidationDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public OrderStatusChangedToPaidValidationDomainEventHandler(IPublishEndpoint publishEndpoint,
        IUnitOfWork unitOfWork)
    {
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(OrderStatusChangedToPaidValidationDomainEvent notification, CancellationToken cancellationToken)
    {
        var eventToPublish = new OrderStatusChangedToPaidIntegrationEvent(notification.Order.BuyerId,
            notification.Order.Id,
            (OrderStatus)notification.Order.OrderStatus);

        await _publishEndpoint.Publish(
            eventToPublish
        );

        await _unitOfWork.CommitTransaction();
    }
}