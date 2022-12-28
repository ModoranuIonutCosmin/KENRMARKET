namespace Gateway.Domain.Models.Orders;

[Serializable]
public class Order
{
    public Guid            Id          { get; set; }
    public Guid            BuyerId     { get; set; }
    public List<OrderItem> OrderItems  { get; set; }
    public string          Promocode   { get; set; }
    public Address         Address     { get; set; }
    public decimal         Total       { get; set; }
    public DateTimeOffset  DateCreated { get; }
    public OrderStatus     OrderStatus { get; set; }
}