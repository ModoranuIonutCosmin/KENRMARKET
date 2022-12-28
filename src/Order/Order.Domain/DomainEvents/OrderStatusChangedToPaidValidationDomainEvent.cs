using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToPaidValidationDomainEvent : IDomainEvent
{
    public OrderStatusChangedToPaidValidationDomainEvent(Entities.Order order,
        Guid id)
    {
        Order      = order;
        CustomerId = id;
    }

    public Entities.Order Order      { get; set; }
    public Guid           CustomerId { get; set; }
}