using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.API.Config;
using Payments.Application.Interfaces;
using Payments.Domain.Models;
using Stripe.Checkout;

namespace Payments.API.Controllers
{
    [ApiVersion("1.0")]
    public class PaymentsController : BaseController
    {
        private readonly IPaymentsGatewayService _paymentsGatewayService;
        private readonly FrontEndInfo _frontEndInfo;
        private readonly StripeSettings _stripeSettings;

        public PaymentsController(IPaymentsGatewayService paymentsGatewayService,
            IOptions<StripeSettings> stripeOptions,
            IOptions<FrontEndInfo> frontEndInfo)
        {
            _stripeSettings = stripeOptions.Value;
            _frontEndInfo = frontEndInfo.Value;
            _paymentsGatewayService = paymentsGatewayService;
        }

        [HttpPost("createCheckoutSession")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] Order order)

        {
            return Ok(
                await _paymentsGatewayService.OpenPaymentCheckoutSessionForOrder(order,
                    _frontEndInfo.OrderSuccessUrl,
                    _frontEndInfo.OrderFailureUrl));
        }
    }
}
