namespace Gateway.Domain.Shared;

public interface IEntity
{
    List<IDomainEvent> DomainEvents { get; }
    Guid Id { get; set; }
    void AddDomainEvent(IDomainEvent domainEvent);
}