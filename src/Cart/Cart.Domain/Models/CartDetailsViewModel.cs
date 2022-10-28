namespace Cart.Domain.Models;

public class CartDetailsViewModel
{
    public string CustomerId { get; set; }
    public List<CartItemDTO> CartItems { get; set; }
    public string Promocode { get; set; }
}