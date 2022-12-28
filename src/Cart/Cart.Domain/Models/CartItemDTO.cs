namespace Cart.Domain.Models;

public record CartItemDTO(Guid ProductId, string ProductName, decimal Quantity, decimal UnitPrice,
    string? PictureUrl, DateTimeOffset AddedAt);