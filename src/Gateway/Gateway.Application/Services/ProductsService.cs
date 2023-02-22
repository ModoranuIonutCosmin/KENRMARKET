using System.Net.Http.Json;
using System.Text.Json;
using Gateway.API.Routes;
using Gateway.Application.Interfaces;
using Gateway.Domain.Models.Products;
using Microsoft.Extensions.Logging;

namespace Gateway.Application.Services;

public class ProductsService : IProductsService
{
    private readonly IHttpClientFactory       _httpClientFactory;
    private readonly ILogger<ProductsService> _logger;

    public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger            = logger;
    }

    public async Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("ProductsService");
            var response   = await httpClient.GetAsync(ServicesRoutes.Products.AllProducts);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var result =
                    JsonSerializer.Deserialize<IEnumerable<Product>>(content, deserializationOptions);

                return (true, result, null);
            }

            return (false, null, $"Failed to query service. Reason: {response.ReasonPhrase} ");
        }

        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());

            return (false, null, ex.Message);
        }
    }

    public async Task<(bool IsOk, Product Product, string ErrorMessage)> GetProductByIdAsync(Guid productId)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("ProductsService");
            var response   = await httpClient.GetAsync(ServicesRoutes.Products.ProductById(productId));

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var result =
                    JsonSerializer.Deserialize<Product>(content, deserializationOptions);

                return (true, result, null);
            }

            return (false, null, $"Failed to query service. Reason: {response.ReasonPhrase} ");
        }

        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());

            return (false, null, ex.Message);
        }
    }


    public async Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsFiltered(
        FilterOptions filterOptions)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient("ProductsService");

            var response = await httpClient.PostAsJsonAsync(ServicesRoutes.Products.FilteredProducts, filterOptions);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var deserializationOptions = new JsonSerializerOptions
                                             {
                                                 PropertyNameCaseInsensitive = true
                                             };

                var result =
                    JsonSerializer.Deserialize<List<Product>>(content, deserializationOptions);

                return (true, result, null);
            }

            return (false, null, $"Failed to query service. Reason: {response.ReasonPhrase} ");
        }

        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());

            return (false, null, ex.Message);
        }
    }
}