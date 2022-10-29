using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToStockValidatedDomainEvent : IDomainEvent
{
    public Guid CustomerId { get; set; }

    public Entities.Order Order { get; set; }

    public OrderStatusChangedToStockValidatedDomainEvent(Entities.Order order,
        Guid id)
    {
        Order = order;
        CustomerId = id;
    }
}