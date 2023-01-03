using Customers.Domain.Entities;

namespace Customers.Infrastructure.Seed;

public class CustomersFactory : ICustomersFactory
{
    public List<Customer> CreateCustomers()
    {
        return new List<Customer>
               {
                   new()
                   {
                       Id          = new Guid("08c4c998-6641-4408-9310-08c1dca3df26"),
                       FirstName   = "Pearlie",
                       LastName    = "Crona",
                       MiddleName  = "",
                       UserName = "someusernamezero530",
                       PhoneNumber = "5166783555",
                       Email       = "AAAAA10110101@gmail.com",
                       BirthDate   = DateTimeOffset.UtcNow,
                       Address = new Address
                                 {
                                     State        = "North Dakota",
                                     Apartment    = "K9",
                                     City         = "Rockville Center",
                                     County       = "",
                                     PostalCode   = "11570",
                                     AddressLine1 = "234 Sunrise Hwy",
                                     AddressLine2 = "234 Sunrise Hwy"
                                 }
                   },
                   new()
                   {
                       Id         = new Guid("acbcb196-0d48-4865-9417-eddb9c1b5ce0"),
                       FirstName  = "Gretchen",
                       LastName   = "Gibson",
                       MiddleName = "",
                       UserName   = "someusernamezero531",
                       PhoneNumber = "7607467866",
                       Email       = "GibsonKREATION101@gmail.com",
                       BirthDate   = DateTimeOffset.UtcNow,
                       Address = new Address
                                 {
                                     State        = "California",
                                     Apartment    = "K6",
                                     City         = "Escondido",
                                     County       = "",
                                     PostalCode   = "92029",
                                     AddressLine1 = "2030 Auto Park Way",
                                     AddressLine2 = "2030 Auto Park Way"
                                 }
                   }
               };
    }
}