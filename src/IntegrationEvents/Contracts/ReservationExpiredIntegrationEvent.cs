using IntegrationEvents.Base;

namespace IntegrationEvents.Contracts;

public class ReservationExpiredIntegrationEvent : IIntegrationEvent
{

    public ReservationExpiredIntegrationEvent(List<(Guid productId, decimal quantity)> reservations,
        Guid customerId,
        Guid orderId)
    {
        Reservations = reservations;
        CustomerId = customerId;

        Id = Guid.NewGuid();
        OrderId = orderId;
    }

    public List<(Guid productId, decimal quantity)> Reservations { get; init; }
    public Guid                                     CustomerId   { get; init; }
    public Guid                                     OrderId      { get; set; }
    public Guid                                     Id           { get; init; }
}