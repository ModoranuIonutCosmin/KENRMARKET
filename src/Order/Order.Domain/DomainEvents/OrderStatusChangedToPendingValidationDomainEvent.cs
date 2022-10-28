using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToPendingValidationDomainEvent : IDomainEvent
{
    public Guid Id { get; set; }

    public Entities.Order Order { get; set; }

    public OrderStatusChangedToPendingValidationDomainEvent(Entities.Order order,
        Guid id)
    {
        Order = order;
        Id = id;
    }
}