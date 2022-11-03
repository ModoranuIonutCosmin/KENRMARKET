using System;

namespace IntegrationEvents.Models;

public class ProductQuantity
{
    public Guid ProductId { get; init; }
    
    public decimal Quantity { get; init; }


    public ProductQuantity(Guid productId, decimal quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}