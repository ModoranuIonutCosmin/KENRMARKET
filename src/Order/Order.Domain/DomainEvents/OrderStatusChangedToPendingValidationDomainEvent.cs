using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToPendingValidationDomainEvent : IDomainEvent
{
    public Guid CustomerId { get; set; }

    public Entities.Order Order { get; set; }

    public OrderStatusChangedToPendingValidationDomainEvent(Entities.Order order,
        Guid customerId)
    {
        Order = order;
        CustomerId = customerId;
    }
}