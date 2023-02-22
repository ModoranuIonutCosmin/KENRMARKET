using Customers.Application.Interfaces.Base;
using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface IReservationsRepository : IRepository<Reservation, Guid>
{
    public Task<Reservation> UpdateReservationsForCustomer(Guid customerId, Reservation reservation
        );

    public Task RemoveReservationForUser(Guid customerId);

    public Task<List<Reservation>> GetAllReservations();
}