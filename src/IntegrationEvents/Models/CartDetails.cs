namespace IntegrationEvents.Models;

[Serializable]
public class CartDetails
{
    public Guid           Id         { get; set; }
    public Guid           CustomerId { get; set; }
    public List<CartItem> CartItems  { get; set; }
    public string         Promocode  { get; set; }
    public decimal        Total      => CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
}