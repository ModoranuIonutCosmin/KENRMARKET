namespace Gateway.Domain.Models.Carts;

[Serializable]
public class CartDetails
{
    // public string            CustomerId { get; set; }
    // public List<CartItemDTO> CartItems  { get; set; }
    // public string            Promocode  { get; set; }
    public Guid           Id         { get; set; }
    public Guid           CustomerId { get; set; }
    public List<CartItem> CartItems  { get; set; }
    public string         Promocode  { get; set; }
    public decimal        Total      => CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
}