using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class ReservationMadeForItemsIntegrationEvent : IIntegrationEvent
{
    public ReservationMadeForItemsIntegrationEvent(List<ProductQuantity> reservations,
        Guid customerId, Guid orderId)
    {
        Reservations = reservations;
        CustomerId   = customerId;
        OrderId = orderId;

        Id = Guid.NewGuid();
    }

    public List<ProductQuantity> Reservations { get; init; }
    public Guid                                     CustomerId   { get; init; }
    public Guid                                     OrderId      { get; init; }
    public Guid Id { get; init; } 
}