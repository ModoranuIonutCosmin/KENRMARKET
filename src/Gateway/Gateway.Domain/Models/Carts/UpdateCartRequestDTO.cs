using Gateway.API.Models;

namespace Gateway.API.Auth.Models.Carts
{
    public class UpdateCartRequestDTO
    {
        public List<CartItem> CartItems { get; set; }
        public string Promocode { get; set; }
    }
}
