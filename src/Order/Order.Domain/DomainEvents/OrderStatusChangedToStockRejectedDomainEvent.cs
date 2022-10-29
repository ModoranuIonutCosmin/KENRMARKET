using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStatusChangedToStockRejectedDomainEvent : IDomainEvent
{
    public Guid CustomerId { get; set; }

    public Entities.Order Order { get; set; }

    public OrderStatusChangedToStockRejectedDomainEvent(Entities.Order order,
        Guid id)
    {
        Order = order;
        CustomerId = id;
    }
}