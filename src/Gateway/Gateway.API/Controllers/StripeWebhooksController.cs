using System.Text;
using Gateway.API.Routes;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/payments/webhook")]
public class StripeWebhooksController : BaseController
{
    private readonly IHttpClientFactory                _httpClientFactory;
    private readonly ILogger<StripeWebhooksController> _logger;

    //TODO: Refactor intr-un serviciu
    public StripeWebhooksController(IHttpClientFactory httpClientFactory,
        ILogger<StripeWebhooksController> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger            = logger;
    }

    [HttpPost]
    public async Task<IActionResult> ForwardStripeEvent()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        _logger.LogInformation("[Stripe event] Gateway forwarding a new stripe event {@payload}", json);

        var headers = Request.Headers
                             .ToDictionary(h => h.Key, h => h.Value);

        var httpClient = _httpClientFactory.CreateClient("PaymentsService");

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (var requestMessage =
               new HttpRequestMessage
               {
                   Method     = HttpMethod.Post,
                   RequestUri = new Uri(httpClient.BaseAddress + ServicesRoutes.Payments.PaymentsWebhook),
                   Headers =
                   {
                       { "Stripe-Signature", headers["Stripe-Signature"].ToString() }
                   },
                   Content = content
               })
        {
            var response = await httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("[Stripe event] Gateway forwarding failed  reason=\"{@reason}\"",
                                       response.ReasonPhrase);

                return BadRequest(response.ReasonPhrase);
            }
        }


        return Ok();
    }
}