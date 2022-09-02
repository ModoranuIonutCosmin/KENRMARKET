using MongoDB.Bson;
using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Seed;

namespace Products.Infrastructure.Data_Access.v1
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DbContext dbContext;

        public ProductsRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;

            if (dbContext.Products.CountDocuments(new BsonDocument()) == 0)
            {
                this.dbContext.Products.InsertMany(new ProductsFactory().CreateProducts());
                this.dbContext.Categories.InsertMany(new ProductsFactory().CreateCategories());
            }
        }
        public async Task<List<Product>> GetAllProducts()
        {
            var products = await (await dbContext.Products.FindAsync(new BsonDocument()))
                           .ToListAsync();

            return products;
        }

            
    }
}

