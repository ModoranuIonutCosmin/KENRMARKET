using Cart.Domain.Entities;

namespace Cart.Domain.Models
{
    public class CartDetailsViewModel
    {
        public string CustomerId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public string Promocode { get; set; }
    }
}

