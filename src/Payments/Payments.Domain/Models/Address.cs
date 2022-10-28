namespace Payments.Domain.Models;

public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string County { get; set; }
    public string Apartment { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
}