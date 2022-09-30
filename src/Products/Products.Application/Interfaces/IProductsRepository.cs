using Products.Domain.Entities;

namespace Products.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> GetProduct(Guid id);
        Task<List<Product>> GetAllProducts();
    }
}

