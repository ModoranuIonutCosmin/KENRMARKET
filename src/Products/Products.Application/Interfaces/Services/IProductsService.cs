using IntegrationEvents.Models;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Interfaces.Services;

public interface IProductsService
{
    Task<List<Product>> GetProducts();
    Task<Product> GetProduct(Guid productId);
    Task<List<Product>> GetProductsFiltered(FilterOptions filterOptions);
    Task<bool> AreProductsOnStock(List<ProductQuantity> productQuantities);
}