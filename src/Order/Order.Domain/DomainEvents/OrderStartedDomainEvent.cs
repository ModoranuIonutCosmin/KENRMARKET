using Order.Domain.Shared;

namespace Order.Domain.DomainEvents;

public class OrderStartedDomainEvent : IDomainEvent
{
    public OrderStartedDomainEvent(Entities.Order order)
    {
        Order = order;

        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    
    public Entities.Order Order { get; set; }
}