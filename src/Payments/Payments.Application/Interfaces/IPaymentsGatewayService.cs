using Payments.Domain.Entities;
using Payments.Domain.Models;

namespace Payments.Application.Interfaces
{
    public interface IPaymentsGatewayService
    {
        public Task RecordAcceptedPayment(Payment payment);
        Task<CheckoutSession> OpenPaymentCheckoutSessionForOrder(Order order, string successRedirectUrl,
            string failureRedirectUrl);
    }
}

