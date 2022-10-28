using Customers.Domain.Entities;

namespace Customers.Infrastructure.Seed;

public interface ICustomersFactory
{
    public List<Customer> CreateCustomers();
}