using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToPaidValidationDomainEvent : IDomainEvent
{
    public Guid CustomerId { get; set; }

    public Entities.Order Order { get; set; }

    public OrderStatusChangedToPaidValidationDomainEvent(Entities.Order order,
        Guid id)
    {
        Order = order;
        CustomerId = id;
    }
}