using Gateway.Domain.Models.Checkout;
using Gateway.Domain.Models.Orders;

namespace Gateway.Application.Interfaces;

public interface IPaymentsService
{
    Task<(bool isOk, CheckoutSession checkoutSession, string errorMessage)> CreateCheckoutSession(Order order);
}