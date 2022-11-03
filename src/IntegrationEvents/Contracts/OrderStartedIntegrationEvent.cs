using System;
using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStartedIntegrationEvent : IIntegrationEvent
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; private set; }

    public OrderStartedIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;

        Id = Guid.NewGuid();
    }
}