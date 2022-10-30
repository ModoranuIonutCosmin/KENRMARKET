using System.Text;
using System.Text.Json;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Gateway.API.Routes;
using Gateway.Domain.Models.Carts;

namespace Gateway.API.Services;

public class PaymentsService : IPaymentsService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<PaymentsService> _logger;

    public PaymentsService(IHttpClientFactory httpClientFactory, ILogger<PaymentsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<(bool isOk, CheckoutSession checkoutSession, string errorMessage)> CreateCheckoutSession
        (Domain.Models.Orders.Order order)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("PaymentsService");
            
            var body = JsonSerializer.Serialize(order);

            var response = await httpClient.PostAsync(ServicesRoutes.Payments.CreateCheckoutSession,
                new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var checkoutSession = JsonSerializer.Deserialize<CheckoutSession>(content, deserializationOptions);

                return (true, checkoutSession, null);
            }

            return (false, null, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.Message);

            return (false, null, ex.Message);
        }
    }
}