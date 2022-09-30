using Products.Application.Interfaces;
using Products.Application.Interfaces.Services;
using Products.Domain.Entities;

namespace Products.Application.Features
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await productsRepository.GetAllProducts();
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            return await productsRepository.GetProduct(productId);
        }
    }
}

