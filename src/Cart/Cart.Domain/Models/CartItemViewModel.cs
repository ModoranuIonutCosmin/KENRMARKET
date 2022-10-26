using Cart.Domain.Entities;

namespace Cart.Domain.Models
{
    public class CartItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string PictureUrl { get; set; }
        public DateTimeOffset AddedAt { get; set; }
    }
}

