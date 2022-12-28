using System.Text;
using System.Text.Json;
using Gateway.API.Routes;
using Gateway.Application.Interfaces;
using Gateway.Domain.Models.Carts;
using Gateway.Domain.Models.Checkout;
using Gateway.Domain.Models.Orders;
using Microsoft.AspNetCore.WebUtilities;

namespace Gateway.API.Services;

public class CartService : ICartService
{
    private readonly IHttpClientFactory   _httpClientFactory;
    private readonly ILogger<CartService> _logger;

    public CartService(IHttpClientFactory httpClientFactory, ILogger<CartService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger            = logger;
    }

    public async Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(Guid customerId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("CartService");

            var uri = QueryHelpers.AddQueryString(ServicesRoutes.Cart.CartDetails,
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

                var cartDetails = JsonSerializer.Deserialize<CartDetails>(content, deserializationOptions);

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
                                                  new Dictionary<string, string?>
                                                  {
                                                      { "customerId", customerId.ToString() }
                                                  });

            var body = JsonSerializer.Serialize(cartDetails);

            var response = await httpClient.PutAsync(uri,
                                                     new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var returnedCartDetails = JsonSerializer.Deserialize<CartDetails>(content, deserializationOptions);

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

    public async Task<(bool IsOk, CartDetailsDTO CartDetails, string ErrorMessage)> CheckoutCart(Guid customerId,
        Address shippingAddress)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("CartService");

            var body = JsonSerializer.Serialize(new CheckoutRequestDTO
                                                {
                                                    Address    = shippingAddress,
                                                    CustomerId = customerId
                                                });

            var response = await httpClient.PostAsync(ServicesRoutes.Cart.Checkout,
                                                      new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var cartDetails = JsonSerializer.Deserialize<CartDetailsDTO>(content, deserializationOptions);

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


    public async Task<(bool IsOk, CartItemDTO CartItem, string ErrorMessage)> AddCartItem(Guid customerId,
        CartItemDTO cartItemDto)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("CartService");

            var uri = QueryHelpers.AddQueryString(ServicesRoutes.Cart.AddItemToCartPost,
                                                  new Dictionary<string, string?>
                                                  {
                                                      { "customerId", customerId.ToString() }
                                                  });

            var body = JsonSerializer.Serialize(cartItemDto);

            var response = await httpClient.PostAsync(uri,
                                                      new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var returnedCartItem = JsonSerializer.Deserialize<CartItemDTO>(content, deserializationOptions);

                return (true, returnedCartItem, null);
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