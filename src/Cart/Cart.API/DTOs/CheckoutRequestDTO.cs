using IntegrationEvents.Models;

namespace Cart.API.DTOs;

public class CheckoutRequestDTO
{
    public Guid CustomerId { get; set; }

    public Address Address { get; set; }
}