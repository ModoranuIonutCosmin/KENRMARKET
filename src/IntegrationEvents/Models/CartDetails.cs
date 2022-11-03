
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationEvents.Models;

[Serializable]
public class CartDetails
{
    public Guid CustomerId { get; set; }
    public List<CartItem> CartItems { get; set; }
    public string Promocode { get; set; }
    public decimal Total => CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
}