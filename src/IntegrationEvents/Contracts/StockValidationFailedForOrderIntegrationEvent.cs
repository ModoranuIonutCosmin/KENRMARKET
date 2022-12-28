using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class StockValidationFailedForOrderIntegrationEvent : IIntegrationEvent
{
    public StockValidationFailedForOrderIntegrationEvent(Guid customerId, Guid orderId, OrderStatus orderStatus)
    {
        CustomerId  = customerId;
        OrderId     = orderId;
        OrderStatus = orderStatus;

        Id = Guid.NewGuid();
    }

    public Guid        OrderId     { get; set; }
    public Guid        CustomerId  { get; }
    public OrderStatus OrderStatus { get; }
    public Guid        Id          { get; init; }
}