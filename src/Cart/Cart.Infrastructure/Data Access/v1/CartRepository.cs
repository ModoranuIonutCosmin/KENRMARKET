using Cart.Application.Interfaces;
using Cart.Domain.Entities;
using Cart.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Data_Access.v1;

public class CartRepository : ICartRepository
{
    private readonly CartsDBContext _cartsDbContext;

    public CartRepository(CartsDBContext cartsDbContext)
    {
        _cartsDbContext = cartsDbContext;

        if (!cartsDbContext.Carts.Any())
        {
            cartsDbContext.Carts.AddRange(new CartDetailsFactory().SeedCartDetails().Result);
            cartsDbContext.SaveChanges();
        }
    }

    public async Task<CartDetails> EnsureCartExists(Guid customerId)
    {
        var cartExists = _cartsDbContext
            .Carts
            .Any(c => c.CustomerId.Equals(customerId));

        if (!cartExists)
        {
            await _cartsDbContext.Carts.AddAsync(new CartDetails
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
        var cart = await _cartsDbContext.Carts.SingleAsync(c => c.CustomerId.Equals(customerId));


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
        var cartDetails = _cartsDbContext.Carts
            .Include(c => c.CartItems)
            .SingleOrDefault(c => c.CustomerId.Equals(customerId));

        cartDetails.CartItems.Add(cartItem);

        await _cartsDbContext.SaveChangesAsync();
    }

    public async Task UpdateCartItem(Guid cartId, CartItem newCartItem)
    {
        var cartItem = _cartsDbContext.CartItems.Single(ci => ci.Id.Equals(cartId));

        _cartsDbContext.Entry(cartItem).CurrentValues.SetValues(newCartItem);

        await _cartsDbContext.SaveChangesAsync();
    }

    public async Task<CartDetails> DeleteCartContents(Guid customerId)
    {
        var cart = await GetCartDetails(customerId);

        if (cart != null)
        {
            cart.CartItems = new List<CartItem>();
        }

        cart.Promocode = "";

        await _cartsDbContext.SaveChangesAsync();

        return cart;
    }

    public async Task<CartDetails> ModifyCart(Guid customerId, CartDetails newCartDetails)
    {
        var shoppingCart = await _cartsDbContext.Carts
            .Include(c => c.CartItems)
            .SingleOrDefaultAsync(c => c.CustomerId.Equals(customerId));

        //TODO: Refactorizat

        if (shoppingCart == null)
        {
            await _cartsDbContext.AddAsync(newCartDetails);

            await _cartsDbContext.SaveChangesAsync();

            return newCartDetails;
        }

        shoppingCart.CartItems = newCartDetails.CartItems;
        shoppingCart.Promocode = newCartDetails.Promocode;

        _cartsDbContext.Carts.Update(shoppingCart);

        await _cartsDbContext.SaveChangesAsync();

        return newCartDetails;
    }
}