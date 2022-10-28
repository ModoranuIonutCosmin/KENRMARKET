using Products.Domain.Entities;

namespace Products.Infrastructure.Seed;

public interface IProductsFactory
{
    public List<Product> CreateProducts();
}