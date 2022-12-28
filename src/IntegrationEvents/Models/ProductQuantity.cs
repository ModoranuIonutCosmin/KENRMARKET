namespace IntegrationEvents.Models;

public class ProductQuantity
{
    public ProductQuantity(Guid productId, decimal quantity)
    {
        ProductId = productId;
        Quantity  = quantity;
    }

    public Guid ProductId { get; init; }

    public decimal Quantity { get; init; }
}