namespace Gateway.Application.Interfaces;

public interface IOrdersAggregatesService
{
    Task<(bool IsSuccess, dynamic CheckoutSession)> GetCheckoutSessionForOrder(Guid customerId, Guid orderId);
}