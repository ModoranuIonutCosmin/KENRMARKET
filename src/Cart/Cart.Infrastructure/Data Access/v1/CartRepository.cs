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

        public async Task<CartDetails> EnsureCartExists(Guid customerId)
        {
            bool cartExists = _cartsDbContext
                .Carts
                .Any(c => c.CustomerId.Equals(customerId));

            if (!cartExists)
            {
                await _cartsDbContext.Carts.AddAsync(new CartDetails()
                {
                    CustomerId = customerId,
                    Promocode = ""
                });

                await _cartsDbContext.SaveChangesAsync();
            }


            return await _cartsDbContext.Carts
                .SingleAsync(c => c.CustomerId.Equals(customerId));
        }

        public async Task<CartDetails> SetCartPromocode(Guid customerId, string promocode)
        {
            CartDetails cart = await _cartsDbContext.Carts.SingleAsync(c => c.CustomerId.Equals(customerId));


            cart.Promocode = promocode;

            await _cartsDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<CartDetails> GetCartDetails(Guid customerId)
        {
            return await _cartsDbContext.Carts
                .Where(c => c.CustomerId.Equals(customerId))
                .Include(c => c.CartItems)
                .SingleAsync();
        }

        public async Task AddCartItem(Guid customerId, CartItem cartItem)
        {
            CartDetails cartDetails = _cartsDbContext.Carts
                .Include(c => c.CartItems)
                .SingleOrDefault(c => c.CustomerId.Equals(customerId));

            cartDetails.CartItems.Add(cartItem);

            await _cartsDbContext.SaveChangesAsync();
        }

        public async Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails)
        {
            var shoppingCart = await _cartsDbContext.Carts
                .SingleOrDefaultAsync(c => c.CustomerId.Equals(customerId));

            //TODO: Refactorizat

            if (shoppingCart == null)
            {
                await _cartsDbContext.AddAsync(newCartDetails);

                await _cartsDbContext.SaveChangesAsync();

                return newCartDetails;
            }

            newCartDetails.CartItems = newCartDetails.CartItems;
            newCartDetails.Promocode = newCartDetails.Promocode;

            _cartsDbContext.Carts.Update(shoppingCart);

            await _cartsDbContext.SaveChangesAsync();

            return newCartDetails;
        }
    }
}

