using Customers.Domain.Entities;

namespace Customers.Infrastructure.Seed;

public class CustomersFactory : ICustomersFactory
{
    public List<Customer> CreateCustomers()
    {

        return new List<Customer>()
        {
            new Customer()
            {
                FirstName = "Pearlie",
                LastName = "Crona",
                MiddleName = "",
                PhoneNumber = "5166783555",
                Email = "AAAAA10110101@gmail.com",
                BirthDate = DateTimeOffset.UtcNow,
                Address = new Address()
                {
                    State = "North Dakota",
                    Apartment = "K9",
                    City = "Rockville Center",
                    County = "",
                    PostalCode = "11570",
                    AddressLine1 = "234 Sunrise Hwy",
                    AddressLine2 = "234 Sunrise Hwy",
                }
            },
            new Customer()
            {
                FirstName = "Gretchen",
                LastName = "Gibson",
                MiddleName = "",
                PhoneNumber = "7607467866",
                Email = "GibsonKREATION101@gmail.com",
                BirthDate = DateTimeOffset.UtcNow,
                Address = new Address()
                {
                    State = "California",
                    Apartment = "K6",
                    City = "Escondido",
                    County = "",
                    PostalCode = "92029",
                    AddressLine1 = "2030 Auto Park Way",
                    AddressLine2 = "2030 Auto Park Way",
                }
            },

        };
    }
}