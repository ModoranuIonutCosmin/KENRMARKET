using Gateway.Domain.Models.Orders;

namespace Gateway.Domain.Models.Checkout;

public class CheckoutRequestDTO
{
    public Guid CustomerId { get; set; }

    public Address Address { get; set; }
}