using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(Guid customerId)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("CartService");

                var uri = QueryHelpers.AddQueryString(ServicesRoutes.Cart.CartDetails, 
                    new Dictionary<string, string?>()
                {
                    { "customerId", customerId.ToString() }
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

        public async Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> UpdateCart(Guid customerId,
            CartDetails cartDetails)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("CartService");

                var uri = QueryHelpers.AddQueryString(ServicesRoutes.Cart.ModifyCartPut,
                    new Dictionary<string, string?>()
                {
                    { "customerId", customerId.ToString() }
                });

                string body = JsonSerializer.Serialize<CartDetails>(cartDetails);

                var response = await httpClient.PutAsync(uri, 
                    new StringContent(body, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var deserializationOptions = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    CartDetails returnedCartDetails = JsonSerializer.Deserialize<CartDetails>(content, deserializationOptions);

                    return (true, returnedCartDetails, null);
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
