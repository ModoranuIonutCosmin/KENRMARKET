using MediatR;

namespace Order.Domain.Shared;

public interface IDomainEvent : INotification
{
    public Guid Id { get; set; }
}