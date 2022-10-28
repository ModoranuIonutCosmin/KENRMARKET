namespace Gateway.Domain.Models.Carts;

[Serializable]
public class CartItem
{
    public Guid? Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string PictureUrl { get; set; }
    public DateTimeOffset AddedAt { get; set; }
    public string CartCustomerId { get; set; }
    public CartDetails? CartDetails { get; set; }
}