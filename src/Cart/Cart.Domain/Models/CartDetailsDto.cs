namespace Cart.Domain.Models;

public record CartDetailsDto(List<CartItemDTO> CartItems, string Promocode);