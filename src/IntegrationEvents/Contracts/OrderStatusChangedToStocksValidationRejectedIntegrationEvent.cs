using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToStocksValidationRejectedIntegrationEvent : IIntegrationEvent
{
    public OrderStatusChangedToStocksValidationRejectedIntegrationEvent(Guid customerId,
        Guid orderId, OrderStatus orderStatus)
    {
        CustomerId  = customerId;
        OrderStatus = orderStatus;
        OrderId     = orderId;

        Id = Guid.NewGuid();
    }

    public Guid        OrderId     { get; set; }
    public Guid        CustomerId  { get; }
    public OrderStatus OrderStatus { get; }
    public Guid        Id          { get; init; }
}