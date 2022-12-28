using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStartedDomainEvent : IDomainEvent
{
    public OrderStartedDomainEvent(Entities.Order order)
    {
        Order = order;

        CustomerId = Guid.NewGuid();
    }

    public Entities.Order Order { get; set; }

    public Guid CustomerId { get; set; }
}