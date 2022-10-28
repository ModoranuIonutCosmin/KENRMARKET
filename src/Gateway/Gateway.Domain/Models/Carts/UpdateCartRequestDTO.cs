namespace Gateway.Domain.Models.Carts;

public class UpdateCartRequestDTO
{
    public List<CartItemDTO> CartItems { get; set; }
    public string Promocode { get; set; }
}