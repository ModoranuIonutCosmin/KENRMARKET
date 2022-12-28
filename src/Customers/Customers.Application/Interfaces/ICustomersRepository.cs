using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface ICustomersRepository
{
    Task<List<Customer>> GetAllCustomersDetails();
    Task<Customer>       GetCustomerDetails(Guid customerId);
}