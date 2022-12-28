using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToPendingValidationDomainEvent : IDomainEvent
{
    public OrderStatusChangedToPendingValidationDomainEvent(Entities.Order order,
        Guid customerId)
    {
        Order      = order;
        CustomerId = customerId;
    }

    public Entities.Order Order      { get; set; }
    public Guid           CustomerId { get; set; }
}