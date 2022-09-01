using Products.Domain.Entities;

namespace Products.Application.Interfaces.Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetProducts();
    }
}

