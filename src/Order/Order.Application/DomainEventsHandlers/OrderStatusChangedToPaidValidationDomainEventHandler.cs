using AutoMapper;
using IntegrationEvents.Contracts;
using IntegrationEvents.Models;
using MassTransit;
using MediatR;
using Order.Application.Interfaces;
using Order.Application.Querries;
using Order.Domain.DomainEvents;

namespace Order.Application.DomainEventsHandlers;

public class OrderStatusChangedToPaidValidationDomainEventHandler :
    DomainEventHandler<OrderStatusChangedToPaidValidationDomainEvent>
{
    private readonly IMapper          _mapper;
    private readonly IMediator        _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUnitOfWork      _unitOfWork;

    public OrderStatusChangedToPaidValidationDomainEventHandler(IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _publishEndpoint = publishEndpoint;
        _mediator        = mediator;
        _unitOfWork      = unitOfWork;
        _mapper          = mapper;
    }

    public override async Task Handle(OrderStatusChangedToPaidValidationDomainEvent notification,
        CancellationToken cancellationToken)
    {
        var order = await _mediator.Send(new QueryOrderByIdCommand(notification.Order.Id));

        var eventToPublish = new OrderStatusChangedToPaidIntegrationEvent(notification.Order.BuyerId,
                                                                          notification.Order.Id,
                                                                          (OrderStatus)notification.Order.OrderStatus,
                                                                          _mapper
                                                                              .Map<IntegrationEvents.Models.
                                                                                  Order>(order)
                                                                         );

        await _publishEndpoint.Publish(
                                       eventToPublish
                                      );

        await _unitOfWork.CommitTransaction();
    }
}