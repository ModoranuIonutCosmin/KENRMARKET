using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToPaidIntegrationEvent : IIntegrationEvent
{
    public OrderStatusChangedToPaidIntegrationEvent(Guid customerId,
        Guid orderId, OrderStatus orderStatus, Order order)
    {
        CustomerId  = customerId;
        OrderStatus = orderStatus;
        Order       = order;
        OrderId     = orderId;

        Id = Guid.NewGuid();
    }

    public Guid        OrderId     { get; set; }
    public Guid        CustomerId  { get; }
    public OrderStatus OrderStatus { get; }

    public Order Order { get; set; }
    public Guid  Id    { get; init; }
}