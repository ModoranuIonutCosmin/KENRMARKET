using System.Text.Json;
using Gateway.API.Routes;
using Gateway.Application.Interfaces;
using Gateway.Domain.Models.Orders;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Gateway.Application.Services;

public class OrdersService : IOrdersService
{
    private readonly IHttpClientFactory     _httpClientFactory;
    private readonly ILogger<OrdersService> _logger;

    public OrdersService(IHttpClientFactory httpClientFactory, ILogger<OrdersService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger            = logger;
    }

    public async Task<(bool isOk, List<Order> ordersDetails, string errorMessage)> GetOrders(Guid customerId)
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

                var orders = JsonSerializer.Deserialize<List<Order>>(content, deserializationOptions);

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

    public async Task<(bool isOk, Order orderDetails, string errorMessage)> GetSpecificOrder(Guid orderId)
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

                var order = JsonSerializer.Deserialize<Order>(content, deserializationOptions);

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