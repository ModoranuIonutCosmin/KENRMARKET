using MongoDB.Bson;
using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Infrastructure.Data_Access.v1
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DbContext dbContext;

        public ProductsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await (await dbContext.Products.FindAsync(new BsonDocument()))
                           .ToListAsync();

            return products;
        }

            
    }
}

