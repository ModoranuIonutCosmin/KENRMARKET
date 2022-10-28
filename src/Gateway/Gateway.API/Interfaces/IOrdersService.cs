namespace Gateway.API.Services;

public interface IOrdersService
{
    Task<(bool isOk, dynamic orderDetails, string errorMessage)> GetOrders(Guid customerId);
}