using Cart.Application.Interfaces;
using Cart.Domain.Entities;
using Cart.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Data_Access.v1
{
    public class CartRepository : ICartRepository
    {
        private readonly CartsDBContext _cartsDbContext;

        public CartRepository(CartsDBContext cartsDbContext)
        {
            this._cartsDbContext = cartsDbContext;

            if (!cartsDbContext.Carts.Any())
            {
                cartsDbContext.Carts.AddRange((new CartDetailsFactory().SeedCartDetails()).Result);
                cartsDbContext.SaveChanges();
            }
        }

        public async Task<CartDetails> EnsureCartExists(string customerId)
        {
            bool cartExists = _cartsDbContext
                .Carts
                .Any(c => c.CustomerId == customerId);

            if (!cartExists)
            {
                await _cartsDbContext.Carts.AddAsync(new CartDetails()
                {
                    CustomerId = customerId,
                    Promocode = ""
                });
            }

            await _cartsDbContext.SaveChangesAsync();

            return await _cartsDbContext.Carts
                .SingleAsync(c => c.CustomerId == customerId);
        }

        public async Task<CartDetails> SetCartPromocode(string customerId, string promocode)
        {
            CartDetails cart = await _cartsDbContext.Carts.SingleAsync(c => c.CustomerId == customerId);


            cart.Promocode = promocode;

            await _cartsDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<CartDetails> GetCartDetails(string customerId)
        {
            return await _cartsDbContext.Carts
                .Where(c => c.CustomerId == customerId)
                .Include(c => c.CartItems)
                .SingleAsync();
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            await _cartsDbContext.CartItems.AddAsync(cartItem);

            await _cartsDbContext.SaveChangesAsync();
        }

            
    }
}

