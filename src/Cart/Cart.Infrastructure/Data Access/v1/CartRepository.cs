using Cart.Application.Interfaces;
using Cart.Domain.Entities;
using Cart.Infrastructure.Data_Access.Base;
using Cart.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cart.Infrastructure.Data_Access.v1;

public class CartRepository : Repository<CartDetails, Guid>, ICartRepository
{
    private readonly CartsDBContext _cartsDbContext;
    private readonly ILogger<CartRepository> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public CartRepository(CartsDBContext cartsDbContext, ILogger<CartRepository> logger,
        IUnitOfWork unitOfWork) : base(cartsDbContext, logger, unitOfWork)
    {
        _cartsDbContext = cartsDbContext;
        _logger = logger;
        _unitOfWork = unitOfWork;

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

        }


        return await _cartsDbContext.Carts
            .SingleAsync(c => c.CustomerId.Equals(customerId));
    }

    public async Task<CartDetails> SetCartPromocode(Guid customerId, string promocode)
    {
        var cart = await _cartsDbContext.Carts.SingleAsync(c => c.CustomerId.Equals(customerId));


        cart.Promocode = promocode;


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

    }

    public async Task UpdateCartItem(Guid cartId, CartItem newCartItem)
    {
        var cartItem = _cartsDbContext.CartItems.Single(ci => ci.Id.Equals(cartId));

        _cartsDbContext.Entry(cartItem).CurrentValues.SetValues(newCartItem);

    }

    public async Task<CartDetails> DeleteCartContents(Guid customerId)
    {
        var cart = await GetCartDetails(customerId);

        if (cart != null)
        {
            cart.CartItems = new List<CartItem>();
        }

        cart.Promocode = "";


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

            return newCartDetails;
        }

        shoppingCart.CartItems = newCartDetails.CartItems;
        shoppingCart.Promocode = newCartDetails.Promocode;

        _cartsDbContext.Carts.Update(shoppingCart);

        return newCartDetails;
    }
}