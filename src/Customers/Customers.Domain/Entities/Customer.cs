using Customers.Domain.Shared;

namespace Customers.Domain.Entities;

public class Customer : Entity
{


    public Customer()
    {
        
    }
    
    public string         FirstName   { get; set; }
    public string         LastName    { get; set; }
    public string?        MiddleName  { get; set; }
    public string         UserName    { get; set; }
    public string         Email       { get; set; }
    public string?        PhoneNumber { get; set; }
    public DateTimeOffset? BirthDate   { get; set; }
    public Address?       Address     { get; set; }
    public Reservation? Reservation { get; set; }
}