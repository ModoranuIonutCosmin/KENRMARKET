namespace Products.Infrastructure.Interfaces;

public interface IMongoDBSettings
{
    string DatabaseName { get; set; }

    string ProductsCollectionName { get; set; }
    string CategoriesCollectionName { get; set; }
    string Host { get; set; }
}