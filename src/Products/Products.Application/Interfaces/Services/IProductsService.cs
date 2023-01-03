using IntegrationEvents.Models;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Interfaces.Services;

public interface IProductsService
{
    Task<List<Product>> GetProducts();
    Task<Product>       GetProduct(Guid productId);
    Task<List<Product>> GetProductsFiltered(FilterOptions filterOptions);
    Task<Product>       AddStocksForProduct(Guid productId, decimal change);
    Task                DeductStocksForProducts(List<(Guid productId, decimal deduction)> deductions);
    Task<bool>          AreProductsOnStock(List<ProductQuantity> productQuantities);
    Task AddStocksForProducts(List<(Guid productId, decimal change)> changes);
}