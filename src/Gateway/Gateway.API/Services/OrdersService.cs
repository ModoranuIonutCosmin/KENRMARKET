using System.Text.Json;
using Gateway.API.Interfaces;
using Gateway.API.Routes;
using Gateway.Domain.Models.Carts;
using Microsoft.AspNetCore.WebUtilities;

namespace Gateway.API.Services;

public class OrdersService : IOrdersService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OrdersService> _logger;

    public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }
    
    public async Task<(bool isOk, List<Domain.Models.Orders.Order> ordersDetails, string errorMessage)> GetOrders(Guid customerId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("OrdersService");

            var uri = QueryHelpers.AddQueryString(ServicesRoutes.Orders.UsersOrders,
                new Dictionary<string, string?>
                {
                    { "customerId", customerId.ToString() }
                });

            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var orders = JsonSerializer.Deserialize<List<Domain.Models.Orders.Order>>(content, deserializationOptions);

                return (true, orders, null);
            }

            return (false, null, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.Message);

            return (false, null, ex.Message);
        }
    }
    
    public async Task<(bool isOk, Domain.Models.Orders.Order orderDetails, string errorMessage)> GetSpecificOrder(Guid orderId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("OrdersService");

            var response = await httpClient.GetAsync(ServicesRoutes.Orders.SpecificOrder(orderId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var order = JsonSerializer.Deserialize<Domain.Models.Orders.Order>(content, deserializationOptions);

                return (true, order, null);
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