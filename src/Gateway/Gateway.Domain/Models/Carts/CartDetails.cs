﻿using Gateway.Domain.Shared;

namespace Gateway.Domain.Models.Carts;

[Serializable]
public class CartDetails : Entity, IAggregateRoot
{
    public Guid CustomerId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public string Promocode { get; set; }
    public decimal Total => CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
}