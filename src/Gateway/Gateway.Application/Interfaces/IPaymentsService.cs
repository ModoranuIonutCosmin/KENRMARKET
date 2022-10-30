
using Gateway.API.Models;

namespace Gateway.API.Interfaces;

public interface IPaymentsService
{
    Task<(bool isOk, CheckoutSession checkoutSession, string errorMessage)> CreateCheckoutSession(Domain.Models.Orders.Order order);
}