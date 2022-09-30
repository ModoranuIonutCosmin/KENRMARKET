using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Domain.Entities;
using Order.Infrastructure.Seed;

namespace Order.Infrastructure.Data_Access.v1
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDBContext _ordersDbContext;

        public OrderRepository(OrdersDBContext ordersDbContext)
        {
            this._ordersDbContext = ordersDbContext;

            if (!ordersDbContext.Carts.Any())
            {
                //ordersDbContext.Carts.AddRange((new CartDetailsFactory().SeedCartDetails()).Result);
                //ordersDbContext.SaveChanges();
            }
        }

        public async Task AddCartItem(OrderItem orderItem)
        {
            //await _ordersDbContext.CartItems.AddAsync(orderItem);

            //await _ordersDbContext.SaveChangesAsync();
        }

            
    }
}

