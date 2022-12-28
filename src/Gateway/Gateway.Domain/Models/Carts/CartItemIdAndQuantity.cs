namespace Gateway.Domain.Models.Carts;

public record CartItemIdAndQuantity(Guid ProductId, decimal Quantity)
{
}