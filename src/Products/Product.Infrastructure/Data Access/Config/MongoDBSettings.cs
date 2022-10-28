using Products.Infrastructure.Interfaces;

namespace Products.Infrastructure.Data_Access.Config;

public class MongoDBSettings : IMongoDBSettings
{
    public MongoDBSettings()
    {
    }

    public MongoDBSettings(string connectionString,
        string databaseName, string productsCollectionName, string categoriesCollectionName)
    {
        DatabaseName = databaseName;
        ProductsCollectionName = productsCollectionName;
        CategoriesCollectionName = categoriesCollectionName;
        Host = connectionString;
    }

    public string DatabaseName { get; set; }
    public string ProductsCollectionName { get; set; }
    public string CategoriesCollectionName { get; set; }
    public string Host { get; set; }
}