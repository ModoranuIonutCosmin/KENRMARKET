using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.API.Config;
using Payments.Application.Commands;
using Stripe;
using Stripe.Checkout;

namespace Payments.API.Controllers;

[ApiVersion("1.0")]
public class PaymentWebHookController : BaseController
{
    private readonly IMediator _mediator;
    private readonly StripeSettings _stripeOptions;

    public PaymentWebHookController(IMediator mediator, IOptions<StripeSettings> stripeOptions)
    {
        _mediator = mediator;
        _stripeOptions = stripeOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], _stripeOptions.WebhookKey);

            switch (stripeEvent.Type)
            {
                case Events.CheckoutSessionCompleted:
                    //TODO: Publica integration event cu payment accepted!

                    var checkoutSession = (Session)stripeEvent.Data.Object;


                    //TODO: DE schimbat la payment intent
                    if (checkoutSession.PaymentStatus != "paid") return Ok();

                    var orderMetadata = checkoutSession.Metadata;

                    var orderId = Guid.Parse(orderMetadata["orderId"]);
                    var payerId = Guid.Parse(orderMetadata["payerId"]);
                    var paymentAmount = decimal.Parse(orderMetadata["paymentAmount"]);
                    var dateFinalized = DateTimeOffset.UtcNow;

                    await _mediator.Send(new NotifyOfAcceptedPaymentCommand
                    {
                        PayerId = payerId,
                        OrderId = orderId,
                        PaymentAmount = paymentAmount,
                        PaymentDate = dateFinalized
                    });


                    break;
                case Events.CheckoutSessionAsyncPaymentFailed:
                    break;
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}