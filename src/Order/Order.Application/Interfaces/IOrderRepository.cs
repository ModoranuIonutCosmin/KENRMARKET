using Order.Application.Interfaces.Base;
using Order.Domain.DataModels;

namespace Order.Application.Interfaces;

public interface IOrderRepository : IRepository<Domain.Entities.Order, Guid>
{
    public Task<List<Domain.Entities.Order>> GetOrdersForUser(Guid customerId);
    public Task<Domain.Entities.Order>       AddNewOrder(Domain.Entities.Order newOrder);
    public Task<Domain.Entities.Order>       GetById(Guid id);

    public Task<Domain.Entities.Order> SetOrderStatus(Guid orderId, OrderStatus newStatus);

    public Task<List<Domain.Entities.Order>> GetOldestUnpaidOrdersFrom(DateTimeOffset toDate,
        OrderStatus orderStatus = OrderStatus.InitialCreation);

    public Task RemoveOrders(List<Domain.Entities.Order> orders);
}