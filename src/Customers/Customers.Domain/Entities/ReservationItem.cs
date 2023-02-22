using Customers.Domain.Shared;

namespace Customers.Domain.Entities;

public class ReservationItem : Entity
{

    public Guid    ProductId { get; private set; }
    public decimal Quantity  { get; private set; }

    protected ReservationItem()
    {
        
    }

    public ReservationItem(Guid productId, decimal quantity)
    {
        ProductId = productId;
        Quantity  = quantity;
    }
}