using System.Text.Json;
using Gateway.API.Interfaces;
using Gateway.API.Models;
using Gateway.API.Routes;

namespace Gateway.API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ProductsService");
                var response = await httpClient.GetAsync(ServicesRoutes.Products.AllProducts);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var deserializationOptions = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var result =
                        JsonSerializer.Deserialize<IEnumerable<Product>>(content, options: deserializationOptions);

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
}
