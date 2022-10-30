using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using MediatR;
using Order.Application.Interfaces;
using Order.Application.Querries;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStatusChangedToPendingValidationDomainEventHandler :
    DomainEventHandler<OrderStatusChangedToPendingValidationDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public OrderStatusChangedToPendingValidationDomainEventHandler(IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IUnitOfWork unitOfWork)
    {
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(OrderStatusChangedToPendingValidationDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var productsAndQuantities = notification.Order.OrderItems
            .Select(oi => new ProductQuantity(oi.ProductId, oi.Quantity));

        var eventToPublish = new OrderStatusChangedToPendingStockValidationIntegrationEvent(notification.Order.BuyerId,
            notification.Order.Id,
            productsAndQuantities.ToList(),
            (OrderStatus)notification.Order.OrderStatus);

        await _publishEndpoint.Publish(
            eventToPublish
        );

        await _unitOfWork.CommitTransaction();
    }
}