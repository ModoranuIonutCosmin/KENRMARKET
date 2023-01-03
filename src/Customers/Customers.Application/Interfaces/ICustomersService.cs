using Customers.Domain.Entities;

namespace Customers.Application.Interfaces;

public interface ICustomersService
{
    Task<List<Customer>> GetAllCustomersDetails();
    Task<Customer>       GetSpecificCustomerDetails(Guid customerId);

    Task<Customer> RegisterNewCustomer(Guid id,
        string firstName,
        string lastName,
        string? middleName,
        string userName,
        string email,
        string? phoneNumber,
        DateTimeOffset? birthDate,
        Address? address);
}