using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Data_Access.Base;
using Customers.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.Data_Access.v1;

public class ReservationsRepository : Repository<Reservation, Guid>, IReservationsRepository
{
    private readonly CustomersDBContext              _customersDbContext;
    private readonly ILogger<ReservationsRepository> _logger;
    private readonly IUnitOfWork                     _unitOfWork;

    public ReservationsRepository(CustomersDBContext customersDbContext, ILogger<ReservationsRepository> logger, IUnitOfWork unitOfWork) : base(customersDbContext, logger, unitOfWork)
    {
        _customersDbContext = customersDbContext;
        _logger             = logger;
        _unitOfWork    = unitOfWork;

        ICustomersFactory factory = new CustomersFactory();

        if (!customersDbContext.Customers.Any())
        {
            customersDbContext.Customers.AddRange(factory.CreateCustomers());
            customersDbContext.SaveChanges();
        }
    }

    public async Task<Reservation> UpdateReservationsForCustomer(Guid customerId,
        Reservation reservation)
    {
        var customer = await _customersDbContext.Customers.SingleAsync(c => c.Id.Equals(customerId));

        customer.Reservation = reservation;

        return reservation;
    }

    public async Task RemoveReservationForUser(Guid customerId)
    {
        var customer = await _customersDbContext.Customers.SingleAsync(c => c.Id.Equals(customerId));

        customer.Reservation = null;
    }

    public async Task<List<Reservation>> GetAllReservations()
    {
        return await _customersDbContext.Reservations
                                        .Include(r => r.ReservationItems)
                                        .ToListAsync();
    }
}