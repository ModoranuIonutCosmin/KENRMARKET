using Products.Domain.Entities;

namespace Products.Application.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProducts();
    }
}

