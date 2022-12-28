using Cart.Domain.Entities;
using Cart.Infrastructure.Interfaces;

namespace Cart.Infrastructure.Seed;

public class CartDetailsFactory : ICartDetailsFactory
{
    public async Task<List<CartDetails>> SeedCartDetails()
    {
        return new List<CartDetails>
               {
                   new()
                   {
                       Promocode  = "BSJ",
                       CustomerId = Guid.Parse("acbcb196-0d48-4865-9417-eddb9c1b5ce0"),
                       CartItems = new List<CartItem>
                                   {
                                       new()
                                       {
                                           AddedAt     = DateTimeOffset.UtcNow,
                                           Quantity    = 10m,
                                           UnitPrice   = 100m,
                                           ProductId   = Guid.Parse("167f928d-1d55-4ee7-bd7a-3b77225e6ce8"),
                                           PictureUrl  = "dummy.png",
                                           ProductName = ""
                                       },
                                       new()
                                       {
                                           AddedAt     = DateTimeOffset.UtcNow,
                                           Quantity    = 20m,
                                           UnitPrice   = 200m,
                                           ProductId   = Guid.Parse("5c199475-ceaa-46db-9ea6-0a8da8f661bf"),
                                           PictureUrl  = "dummy.png",
                                           ProductName = ""
                                       }
                                   }
                   }
               };
    }
}