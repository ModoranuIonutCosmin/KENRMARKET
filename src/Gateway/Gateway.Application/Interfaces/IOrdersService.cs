namespace Gateway.API.Interfaces;

public interface IOrdersService
{
    Task<(bool isOk, List<Domain.Models.Orders.Order> ordersDetails, string errorMessage)> GetOrders(Guid customerId);
    Task<(bool isOk, Domain.Models.Orders.Order orderDetails, string errorMessage)> GetSpecificOrder(Guid orderId);
}