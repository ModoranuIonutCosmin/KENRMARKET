using Gateway.API.Models;
using Order.Domain.Entities;

namespace Gateway.Domain.Models.Orders;

[Serializable]
public class Order
{
    public Guid BuyerId { get; }
    public List<OrderItem> OrderItems { get; set; }
    public string Promocode { get; private set; }
    public Address Address { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset DateCreated { get; }
    public OrderStatus OrderStatus { get; private set; }
}