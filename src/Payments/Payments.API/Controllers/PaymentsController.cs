using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.API.Config;
using Payments.Application.Interfaces;
using Payments.Domain.Models;

namespace Payments.API.Controllers;

[ApiVersion("1.0")]
public class PaymentsController : BaseController
{
    private readonly FrontEndInfo                _frontEndInfo;
    private readonly ILogger<PaymentsController> _logger;
    private readonly IPaymentsGatewayService     _paymentsGatewayService;

    public PaymentsController(IPaymentsGatewayService paymentsGatewayService,
        IOptions<FrontEndInfo> frontEndInfo, ILogger<PaymentsController> logger)
    {
        _frontEndInfo           = frontEndInfo.Value;
        _paymentsGatewayService = paymentsGatewayService;
        _logger                 = logger;
    }

    [HttpPost("createCheckoutSession")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] Order order)

    {
        _logger.LogInformation("Atempting to open checkout session at {@Now}", DateTimeOffset.UtcNow);

        var result = await _paymentsGatewayService.OpenPaymentCheckoutSessionForOrder(order,
         _frontEndInfo.OrderSuccessUrl,
         _frontEndInfo.OrderFailureUrl);

        _logger.LogInformation("Opened a checkout session with id {@CheckoutSessionUrl}", result.CheckoutUrl);

        return Ok(result);
    }
}