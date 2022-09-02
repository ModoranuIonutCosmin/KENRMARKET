namespace Cart.Domain.Entities
{
    [Serializable]
    public class CartDetails
    {
        public string CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string Promocode { get; set; }
        public decimal Total => CartItems.Sum(ci => ci.Quantity * ci.UnitPrice);
    }
}

