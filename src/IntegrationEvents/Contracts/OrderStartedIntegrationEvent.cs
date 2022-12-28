using IntegrationEvents.Base;

namespace IntegrationEvents.Contracts;

public class OrderStartedIntegrationEvent : IIntegrationEvent
{
    public OrderStartedIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;

        Id = Guid.NewGuid();
    }

    public Guid CustomerId { get; }
    public Guid Id         { get; init; }
}