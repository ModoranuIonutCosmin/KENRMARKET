using System;
using System.Collections.Generic;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToPendingStockValidationIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; private set; }
    public OrderStatus OrderStatus { get; private set; }
    public List<ProductQuantity> OrderProducts { get; private set; }
    public OrderStatusChangedToPendingStockValidationIntegrationEvent(Guid customerId, 
        Guid orderId,
        List<ProductQuantity> orderProducts, OrderStatus orderStatus)
    {
        CustomerId = customerId;
        OrderProducts = orderProducts;
        OrderStatus = orderStatus;
        OrderId = orderId;

        Id = Guid.NewGuid();
    }
}