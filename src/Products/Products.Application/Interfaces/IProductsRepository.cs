using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Interfaces;

public interface IProductsRepository
{
    Task<Product> GetProduct(Guid id);
    Task<List<Product>> GetAllProducts();
    Task<List<Product>> FilterProducts(FilterOptions filterOptions);
}