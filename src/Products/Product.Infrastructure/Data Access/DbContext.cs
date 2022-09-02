using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Infrastructure.Interfaces;
using Products.Infrastructure.Mappings;
using Products.Infrastructure.Seed;

namespace Products.Infrastructure.Data_Access
{
    public class DbContext
    {
        private IMongoDatabase Database { get; set; }
        public IMongoCollection<Product> Products { get; set; }
        public IMongoCollection<Category> Categories { get; set; }

        public DbContext(IMongoDBSettings settings)
        {
            var client = new MongoClient(settings.Host);

            ConfigureModelMappings();

            Database = client.GetDatabase(settings.DatabaseName);

            Products = Database.GetCollection<Product>(settings.ProductsCollectionName);
            Categories = Database.GetCollection<Category>(settings.CategoriesCollectionName);

            if (Products.CountDocuments(new BsonDocument()) == 0)
            {
                var factory = new ProductsFactory();

                Products.InsertManyAsync(factory.CreateProducts());
                Categories.InsertManyAsync(factory.CreateCategories());
            }
        }

        private void ConfigureModelMappings()
        {
            CategoriesMappings.Map();
            ProductMappings.Map();
        }
    }
}

