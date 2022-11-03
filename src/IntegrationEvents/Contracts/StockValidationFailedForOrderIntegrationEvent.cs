using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class StockValidationFailedForOrderIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    public StockValidationFailedForOrderIntegrationEvent(Guid customerId, Guid orderId, OrderStatus orderStatus)
    {
        CustomerId = customerId;
        OrderId = orderId;
        OrderStatus = orderStatus;

        Id = Guid.NewGuid();
    }
}