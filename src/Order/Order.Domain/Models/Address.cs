using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Models;

public class Address
{
    [Required] public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    [Required] public string County { get; set; }

    [Required] public string Apartment { get; set; }


    [Required] public string City { get; set; }


    [Required] public string State { get; set; }


    [Required] public string PostalCode { get; set; }
}