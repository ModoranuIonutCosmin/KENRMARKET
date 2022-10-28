using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Shared;

public class Entity
{
    public Guid Id { get; set; }

    [NotMapped] public List<IDomainEvent> DomainEvents { get; } = new();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }
}