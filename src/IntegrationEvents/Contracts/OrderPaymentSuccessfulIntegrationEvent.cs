using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderPaymentSuccessfulIntegrationEvent : IIntegrationEvent
{
    public Guid PayerId { get; set; }

    public Guid OrderId { get; set; }
    
    
    public OrderStatus OrderStatus { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTimeOffset DateFinalized { get; set; }
    public Guid Id { get; init; }

    public OrderPaymentSuccessfulIntegrationEvent(decimal paymentAmount, Guid orderId, DateTimeOffset dateFinalized,
        Guid payerId, OrderStatus orderStatus)
    {
        PaymentAmount = paymentAmount;
        OrderId = orderId;
        DateFinalized = dateFinalized;
        PayerId = payerId;
        OrderStatus = orderStatus;

        Id = Guid.NewGuid();
    }
}