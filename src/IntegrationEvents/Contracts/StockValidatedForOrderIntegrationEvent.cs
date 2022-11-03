using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class StockValidatedForOrderIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid OrderId { get; set; }
    
    public OrderStatus OrderStatus { get; private set; }
    public Guid CustomerId { get; private set; }

    public StockValidatedForOrderIntegrationEvent(Guid customerId, Guid orderId, OrderStatus orderStatus)
    {
        CustomerId = customerId;
        OrderId = orderId;
        OrderStatus = orderStatus;

        Id = Guid.NewGuid();
    }
}