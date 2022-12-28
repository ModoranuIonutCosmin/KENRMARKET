using IntegrationEvents.Base;
using IntegrationEvents.Models;

namespace IntegrationEvents.Contracts;

public class OrderStatusChangedToPendingStockValidationIntegrationEvent : IIntegrationEvent
{
    public OrderStatusChangedToPendingStockValidationIntegrationEvent(Guid customerId,
        Guid orderId,
        List<ProductQuantity> orderProducts, OrderStatus orderStatus)
    {
        CustomerId    = customerId;
        OrderProducts = orderProducts;
        OrderStatus   = orderStatus;
        OrderId       = orderId;

        Id = Guid.NewGuid();
    }

    public Guid                  OrderId       { get; set; }
    public Guid                  CustomerId    { get; }
    public OrderStatus           OrderStatus   { get; }
    public List<ProductQuantity> OrderProducts { get; }
    public Guid                  Id            { get; init; }
}