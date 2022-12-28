using Gateway.Domain.Models.Orders;

namespace Gateway.Application.Interfaces;

public interface IOrdersService
{
    Task<(bool isOk, List<Order> ordersDetails, string errorMessage)> GetOrders(Guid customerId);
    Task<(bool isOk, Order orderDetails, string errorMessage)>        GetSpecificOrder(Guid orderId);
}