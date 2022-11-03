using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToStocksValidatedIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    public OrderStatusChangedToStocksValidatedIntegrationEvent(Guid customerId, 
        Guid orderId, OrderStatus orderStatus)
    {
        CustomerId = customerId;
        OrderStatus = orderStatus;
        OrderId = orderId;

        Id = Guid.NewGuid();
    }
}