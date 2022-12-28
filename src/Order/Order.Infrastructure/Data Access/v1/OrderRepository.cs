using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Domain.DataModels;
using Order.Infrastructure.Data_Access.Base;

namespace Order.Infrastructure.Data_Access.v1;

public class OrderRepository : Repository<Domain.Entities.Order, Guid>, IOrderRepository
{
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(OrdersDBContext ordersDbContext, ILogger<OrderRepository> logger, IUnitOfWork unitOfWork)
        : base(ordersDbContext, logger, unitOfWork)
    {
        _logger = logger;

        if (!ordersDbContext.Orders.Any())
        {
            //ordersDbContext.Orders.AddRange((new CartDetailsFactory().SeedCartDetails()).Result);
            //ordersDbContext.SaveChanges();
        }
    }

    public async Task<List<Domain.Entities.Order>> GetOrdersForUser(Guid customerId)
    {
        return await _ordersDbContext.Orders
                                     .Where(o => o.BuyerId.Equals(customerId))
                                     .Include(o => o.OrderItems)
                                     .OrderByDescending(o => o.DateCreated)
                                     .ToListAsync();
    }

    public async Task<Domain.Entities.Order> AddNewOrder(Domain.Entities.Order newOrder)
    {
        _ordersDbContext.Orders.Add(newOrder);

        return newOrder;
    }

    public async Task<Domain.Entities.Order> GetById(Guid orderId)
    {
        return await _ordersDbContext.Orders
                                     .Include(o => o.OrderItems)
                                     .SingleOrDefaultAsync(o => o.Id.Equals(orderId));
    }

    public async Task<Domain.Entities.Order> SetOrderStatus(Guid orderId, OrderStatus newStatus)
    {
        var order = await GetById(orderId);

        order.SetOrderStatus(newStatus);

        return order;
    }

    public async Task<List<Domain.Entities.Order>> GetOldestUnpaidOrdersFrom(DateTimeOffset toDate,
        OrderStatus orderStatus = OrderStatus.InitialCreation)
    {
        return await _ordersDbContext.Orders
                                     .Where(o => o.OrderStatus == orderStatus &&
                                                 o.DateCreated <= toDate)
                                     .ToListAsync();
    }

    public async Task RemoveOrders(List<Domain.Entities.Order> orders)
    {
        _ordersDbContext.Orders.RemoveRange(orders);
    }
}