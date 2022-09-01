namespace Gateway.Domain.Models
{
    [Serializable]
    public class CartDetails
    {
        public string CustomerId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string Promocode { get; set; }

        public decimal Total { get; set; }
    }
}

