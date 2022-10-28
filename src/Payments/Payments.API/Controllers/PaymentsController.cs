using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Payments.API.Config;
using Payments.Application.Interfaces;
using Payments.Domain.Models;

namespace Payments.API.Controllers;

[ApiVersion("1.0")]
public class PaymentsController : BaseController
{
    private readonly FrontEndInfo _frontEndInfo;
    private readonly IPaymentsGatewayService _paymentsGatewayService;

    public PaymentsController(IPaymentsGatewayService paymentsGatewayService,
        IOptions<FrontEndInfo> frontEndInfo)
    {
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