using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface ICustomersService
{
    Task<List<Customer>> GetAllCustomersDetails();
    Task<Customer> GetSpecificCustomerDetails(Guid customerId);
}