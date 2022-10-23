namespace Order.Domain.Entities
{
    [Serializable]
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset AddedAt { get; set; }
        public void AddToQuantity(decimal quantity)
        {
            Quantity += quantity;
        }
    }
}

