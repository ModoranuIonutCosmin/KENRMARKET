using Customers.Domain.Shared;

namespace Customers.Domain.Entities;

public class Reservation : Entity
{
    public Guid CustomerId { get; private set; }

    public Guid OrderId { get; private set; }
    public DateTimeOffset ReservedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public bool Canceled { get; private set; }
    public List<ReservationItem> ReservationItems { get; private set; }


    protected Reservation()
    {

    }
    public Reservation(Guid customerId, DateTimeOffset reservedAt, DateTimeOffset expiresAt, bool canceled, List<ReservationItem> reservationItems, Guid orderId)
    {
        CustomerId = customerId;
        ReservedAt = reservedAt;
        ExpiresAt = expiresAt;
        Canceled = canceled;
        ReservationItems = reservationItems;
        OrderId = orderId;
    }
}