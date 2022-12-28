using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class StockValidatedForOrderIntegrationEvent : IIntegrationEvent
{
    public StockValidatedForOrderIntegrationEvent(Guid customerId, Guid orderId, OrderStatus orderStatus)
    {
        CustomerId  = customerId;
        OrderId     = orderId;
        OrderStatus = orderStatus;

        Id = Guid.NewGuid();
    }

    public Guid OrderId { get; set; }

    public OrderStatus OrderStatus { get; }
    public Guid        CustomerId  { get; }
    public Guid        Id          { get; init; }
}