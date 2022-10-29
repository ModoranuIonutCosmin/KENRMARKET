using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using Order.Application.Interfaces;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStartedDomainEventHandler :
    DomainEventHandler<OrderStartedDomainEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork _unitOfWork;

    public OrderStartedDomainEventHandler(IPublishEndpoint publishEndpoint, IUnitOfWork unitOfWork)
    {
        _publishEndpoint = publishEndpoint;
        _unitOfWork = unitOfWork;
    }

    public override async Task Handle(OrderStartedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        await _publishEndpoint.Publish(
            new OrderStartedIntegrationEvent(notification.Order.BuyerId)
        );

        await _unitOfWork.CommitTransaction();
    }
}