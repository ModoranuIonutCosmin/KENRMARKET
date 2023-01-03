using Products.Application.Interfaces.Base;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Interfaces;

public interface IProductsRepository : IRepository<Product, Guid>
{
    Task<Product>       GetProduct(Guid id);
    Task<List<Product>> GetAllProducts();

    Task<List<Product>> GetAllProductsWithIdsInList(List<Guid> productIds);
    Task<List<Product>> FilterProducts(FilterOptions filterOptions);

    Task<Product> AddProductStock(Guid productID, decimal amount);
}