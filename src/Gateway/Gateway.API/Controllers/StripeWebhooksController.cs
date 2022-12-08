using System.Net.Http.Headers;
using System.Text;
using Gateway.API.Routes;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/{version:apiVersion}/payments/webhook")]
public class StripeWebhooksController : BaseController
{
    private readonly IHttpClientFactory _httpClientFactory;

    //TODO: Refactor intr-un serviciu
    public StripeWebhooksController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<IActionResult> ForwardStripeEvent()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        var headers = Request.Headers
            .ToDictionary(h => h.Key, h => h.Value);

        var httpClient = _httpClientFactory.CreateClient("PaymentsService");

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using (var requestMessage =
               new HttpRequestMessage
               {
                   Method = HttpMethod.Post,
                   RequestUri = new Uri(httpClient.BaseAddress + ServicesRoutes.Payments.PaymentsWebhook),
                   Headers =
                   {
                       {"Stripe-Signature", headers["Stripe-Signature"].ToString()}
                   },
                   Content = content
               })
        {
            await httpClient.SendAsync(requestMessage);
        }

        return Ok();
    }
}