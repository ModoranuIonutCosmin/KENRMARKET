using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class CheckoutAcceptedIntegrationEvent : IIntegrationEvent
{
    public CheckoutAcceptedIntegrationEvent(Guid customerId, List<CartItem> cartItems, Address address)
    {
        CustomerId = customerId;
        CartItems  = cartItems;
        Address    = address;

        Id = Guid.NewGuid();
    }

    public Guid CustomerId { get; set; }

    public Address        Address   { get; set; }
    public List<CartItem> CartItems { get; set; }
    public Guid           Id        { get; init; }
}