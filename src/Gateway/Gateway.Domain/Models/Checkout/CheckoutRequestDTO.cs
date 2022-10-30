
using Gateway.Domain.Models.Orders;

namespace Gateway.API.Models;

public class CheckoutRequestDTO
{
    public Guid CustomerId { get; set; }
    
    public Address Address { get; set; }
}