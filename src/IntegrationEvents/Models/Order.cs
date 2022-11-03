using System;
using System.Collections.Generic;

namespace IntegrationEvents.Models;

[Serializable]
public class Order
{
    public List<OrderItem> OrderItems { get; set; }
    public Guid BuyerId { get; set; }
    public string Promocode { get; set; }
    public Address Address { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal Total { get; set; }
}