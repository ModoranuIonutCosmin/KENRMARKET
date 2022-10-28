using Cart.Domain.Entities;

namespace Cart.API.DTOs;

public class UpdateCartRequestDTO
{
    public List<CartItem> CartItems { get; set; }
    public string Promocode { get; set; }
}