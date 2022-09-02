namespace Cart.Domain.Entities
{
    [Serializable]
    public class CartItem
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset AddedAt { get; set; }
        public string CartCustomerId { get; set; }
    }
}

