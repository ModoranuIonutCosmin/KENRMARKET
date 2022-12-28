using Payments.Domain.Shared;

namespace Payments.Domain.Entities;

public class Payment : Entity, IAggregateRoot
{
    public Guid           OrderId       { get; set; }
    public Guid           PayerId       { get; set; }
    public DateTimeOffset PaymentDate   { get; set; }
    public decimal        PaymentAmount { get; set; }
}