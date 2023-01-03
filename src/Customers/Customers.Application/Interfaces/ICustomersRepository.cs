using Customers.Application.Interfaces.Base;
using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface ICustomersRepository : IRepository<Customer, Guid>
{
    Task<List<Customer>> GetAllCustomersDetails();
    Task<Customer>       GetCustomerDetails(Guid customerId);
}