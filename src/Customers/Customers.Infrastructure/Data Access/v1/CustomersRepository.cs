using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure.Data_Access.v1;

public class CustomersRepository : ICustomersRepository
{
    private readonly CustomersDBContext _customersDbContext;

    public CustomersRepository(CustomersDBContext customersDbContext)
    {
        _customersDbContext = customersDbContext;

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