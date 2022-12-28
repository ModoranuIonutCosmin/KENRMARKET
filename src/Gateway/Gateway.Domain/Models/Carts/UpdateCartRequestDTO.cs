namespace Gateway.Domain.Models.Carts;

public record UpdateCartRequestDTO(List<CartItemIdAndQuantity> CartItems, string Promocode);