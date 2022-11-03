using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToPaidIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    
    public Order Order { get; set; }

    public OrderStatusChangedToPaidIntegrationEvent(Guid customerId, 
        Guid orderId, OrderStatus orderStatus, Order order)
    {
        CustomerId = customerId;
        OrderStatus = orderStatus;
        Order = order;
        OrderId = orderId;

        Id = Guid.NewGuid();
    }
}