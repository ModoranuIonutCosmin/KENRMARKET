using Cart.Domain.Shared;

namespace Cart.Domain.Entities;

[Serializable]
public class CartItem : Entity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string PictureUrl { get; set; }
    public DateTimeOffset AddedAt { get; set; }
}