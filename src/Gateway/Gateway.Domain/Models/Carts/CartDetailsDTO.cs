namespace Gateway.Domain.Models.Carts;

public class CartDetailsDTO
{
    public List<CartItemDTO> CartItems { get; set; }
    public string            Promocode { get; set; }
}