using Payments.Application.Interfaces;
using Payments.Domain.Entities;
using Payments.Domain.Models;
using Stripe.Checkout;

namespace Payments.Application.Payments;

public class PaymentsGatewayService : IPaymentsGatewayService
{
    private readonly IPaymentsRepository _paymentsRepository;

    public PaymentsGatewayService(IPaymentsRepository paymentsRepository)
    {
        _paymentsRepository = paymentsRepository;
    }

    public async Task RecordAcceptedPayment(Payment payment)
    {
        await _paymentsRepository.AddPaymentAsync(payment);
    }

    public async Task<CheckoutSession> OpenPaymentCheckoutSessionForOrder(Order order, string successRedirectUrl,
        string failureRedirectUrl)
    {
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.Total * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Order from KENRMARKET"
                        }
                    },
                    Quantity = 1
                }
            },
            Metadata = new Dictionary<string, string>
            {
                { "orderId", order.Id.ToString() },
                { "payerId", order.BuyerId.ToString() },
                { "paymentAmount", order.Total.ToString() }
            },
            Mode = "payment",
            SuccessUrl = successRedirectUrl,
            CancelUrl = failureRedirectUrl
        };

        var service = new SessionService();
        var session = service.Create(options);

        return new CheckoutSession
        {
            CheckoutUrl = session.Url
        };
    }
}