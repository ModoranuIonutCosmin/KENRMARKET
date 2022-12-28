using MongoDB.Bson;
using MongoDB.Driver;
using Products.Domain.Entities;
using Products.Infrastructure.Interfaces;
using Products.Infrastructure.Mappings;
using Products.Infrastructure.Seed;

namespace Products.Infrastructure.Data_Access;

public class ProductsDbContext
{
    private readonly IMongoClient   _mongoClient;
    private readonly IMongoDatabase _mongoDatabase;


    public ProductsDbContext(IMongoClient mongoClient, IMongoDatabase mongoDatabase,
        IMongoDBSettings settings
    )
    {
        _mongoClient   = mongoClient;
        _mongoDatabase = mongoDatabase;

        ConfigureModelMappings();

        Products   = _mongoDatabase.GetCollection<Product>(settings.ProductsCollectionName);
        Categories = _mongoDatabase.GetCollection<Category>(settings.CategoriesCollectionName);

        if (Products.CountDocuments(new BsonDocument()) == 0)
        {
            var factory = new ProductsFactory();

            Products.InsertManyAsync(factory.CreateProducts());
            Categories.InsertManyAsync(factory.CreateCategories());
        }
    }

    public IMongoCollection<Product>  Products   { get; set; }
    public IMongoCollection<Category> Categories { get; set; }

    private void ConfigureModelMappings()
    {
        BaseEntityMappings.Map();
        CategoriesMappings.Map();
        ProductMappings.Map();
    }
}