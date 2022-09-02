using System.Text.Json;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Gateway.API.Routes;
using Microsoft.AspNetCore.WebUtilities;

namespace Gateway.API.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CartService> _logger;

        public CartService(IHttpClientFactory httpClientFactory, ILogger<CartService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(string customerId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("CartService");

                var uri = QueryHelpers.AddQueryString(ServicesRoutes.Cart.CartDetails, 
                    new Dictionary<string, string?>()
                {
                    { "customerId", customerId }
                });

                var response = await httpClient.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var deserializationOptions = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    CartDetails cartDetails = JsonSerializer.Deserialize<CartDetails>(content, deserializationOptions);

                    return (true, cartDetails, null);
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
}
