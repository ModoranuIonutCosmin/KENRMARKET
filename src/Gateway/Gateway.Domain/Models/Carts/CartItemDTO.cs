namespace Gateway.API.Auth.Models.Carts
{
    public class CartItemDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset AddedAt { get; set; }
    }
}
