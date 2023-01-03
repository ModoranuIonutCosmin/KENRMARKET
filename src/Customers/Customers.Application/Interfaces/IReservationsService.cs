using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface IReservationsService
{
    public Task MakeReservationsForCustomer(Guid customerId, DateTimeOffset createdAt, DateTimeOffset expiresAt,
        bool isCanceled, List<ReservationItem> reservationItems, Guid orderId);

    public Task<List<Reservation>> GetAllReservations();

    Task<int> InvalidateExpiredReservations();
    Task RemoveReservationByCustomerId(Guid customerId);
}