using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Data_Access.Base;
using Customers.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.Data_Access.v1;

public class CustomersRepository : Repository<Customer, Guid>, ICustomersRepository
{
    private readonly CustomersDBContext           _customersDbContext;
    private readonly ILogger<CustomersRepository> _logger;
    private readonly IUnitOfWork                  _unitOfWork;

    public CustomersRepository(CustomersDBContext customersDbContext, ILogger<CustomersRepository> logger, IUnitOfWork unitOfWork) : base(customersDbContext, logger, unitOfWork)
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

    public async Task<List<Customer>> GetAllCustomersDetails()
    {
        return await _customersDbContext.Customers
                                        .Include(c => c.Address)
                                        .ToListAsync();
    }

    public async Task<Customer> GetCustomerDetails(Guid customerId)
    {
        return await _customersDbContext
                     .Customers
                     .Include(c => c.Address)
                     .SingleOrDefaultAsync(c => c.Id.Equals(customerId));
    }
}