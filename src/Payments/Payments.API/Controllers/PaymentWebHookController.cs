using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.API.Config;
using Payments.Application.Commands;
using Stripe;
using Stripe.Checkout;

namespace Payments.API.Controllers;

[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/webhook")]
public class PaymentWebHookController : BaseController
{
    private readonly ILogger<PaymentWebHookController> _logger;
    private readonly IMediator                         _mediator;
    private readonly StripeSettings                    _stripeOptions;

    public PaymentWebHookController(IMediator mediator, IOptions<StripeSettings> stripeOptions,
        ILogger<PaymentWebHookController> logger)
    {
        _mediator      = mediator;
        _logger        = logger;
        _stripeOptions = stripeOptions.Value;
    }

    [HttpPost]
    public async Task<IActionResult> Webhook()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        _logger.LogInformation("Received a new event from stripe");

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(json,
                                                          Request.Headers["Stripe-Signature"],
                                                          _stripeOptions.WebhookKey);

            _logger.LogInformation("The new event from stripe has id={@id}, type={@type}",
                                   stripeEvent.Id,
                                   stripeEvent.Type);


            switch (stripeEvent.Type)
            {
                case Events.CheckoutSessionCompleted:
                    //TODO: Publica integration event cu payment accepted!

                    var checkoutSession = (Session)stripeEvent.Data.Object;


                    //TODO: DE schimbat la payment intent
                    if (checkoutSession.PaymentStatus != "paid")
                    {
                        return Ok();
                    }

                    var orderMetadata = checkoutSession.Metadata;

                    var orderId       = Guid.Parse(orderMetadata["orderId"]);
                    var payerId       = Guid.Parse(orderMetadata["payerId"]);
                    var paymentAmount = decimal.Parse(orderMetadata["paymentAmount"]);
                    var dateFinalized = DateTimeOffset.UtcNow;

                    _logger.LogInformation("Updating order as paid for order id={@id}, payerId={@payerId}, sum = {@total}",
                                           orderId,
                                           payerId,
                                           paymentAmount);

                    await _mediator.Send(new NotifyOfAcceptedPaymentCommand
                                         {
                                             PayerId       = payerId,
                                             OrderId       = orderId,
                                             PaymentAmount = paymentAmount,
                                             PaymentDate   = dateFinalized
                                         });

                    _logger.LogInformation("Payment succeded notification sent for order id={@id}, payerId={@payerId}, sum = {@total}",
                                           orderId,
                                           payerId,
                                           paymentAmount);

                    break;
                case Events.CheckoutSessionAsyncPaymentFailed:
                    break;
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
    }
}