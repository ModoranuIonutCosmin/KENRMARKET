using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Models;
using Products.Infrastructure.Data_Access.Base;
using Products.Infrastructure.Seed;

namespace Products.Infrastructure.Data_Access.v1;

public class ProductsRepository : Repository<Product, Guid>, IProductsRepository
{
    private readonly ProductsDbContext _productsDbContext;

    public ProductsRepository(ProductsDbContext productsDbContext, ILogger<ProductsRepository> logger)
        : base(productsDbContext, logger)
    {
        this._productsDbContext = productsDbContext;

        if (productsDbContext.Products.CountDocuments(new BsonDocument()) == 0)
        {
            this._productsDbContext.Products.InsertMany(new ProductsFactory().CreateProducts());
            this._productsDbContext.Categories.InsertMany(new ProductsFactory().CreateCategories());
        }
    }

    public async Task<Product> GetProduct(Guid id)
    {
        return await _productsDbContext.Products
            .Find(Builders<Product>.Filter.Eq("_id", id))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await (await _productsDbContext.Products.FindAsync(new BsonDocument()))
            .ToListAsync();

        return products;
    }

    public async Task<List<Product>> GetAllProductsWithIdsInList(List<Guid> productIds)
    {
        //TODO: Test
        var productsCollection = _productsDbContext.Products;

        var filter = Builders<Product>.Filter
            .In(p => p.Id, productIds);

        return await productsCollection
            .Find(filter)
            .ToListAsync();
    }

    public async Task<List<Product>> FilterProducts(FilterOptions filterOptions)
    {
        var builder = Builders<Product>.Filter;

        var filter = builder.Empty;

        if (filterOptions.UsesPriceRangeFilter)
        {
            var betweenRangeFilter = builder.And(
                builder.Gte(x => x.Price, filterOptions.MinPrice),
                builder.Lte(x => x.Price, filterOptions.MaxPrice)
            );

            filter &= betweenRangeFilter;
        }

        var categories = filterOptions.Categories.Select(c => c.Name).ToList();

        if (filterOptions.Categories.Count > 0)
        {
            var oneOfCategoriesList = builder
                .In(p => p.Category.Name, categories);

            filter &= oneOfCategoriesList;
        }


        //TODO: Filter dupa specificatii

        // if (filterOptions.SpecificationsFilters.Count() > 0)
        // {
        //     foreach (var specification in filterOptions.SpecificationsFilters)
        //     {
        //         // var specificationFilter = builder.
        //     }
        // }


        return (await _productsDbContext.Products.FindAsync(filter)).ToList();
    }

    public async Task<Product> DeductProductStock(Guid productID, decimal amount)
    {
        var collection = _productsDbContext.Products;

        var filter = Builders<Product>.Filter.Eq(x => x.Id, productID);
        var updateDefinition = Builders<Product>.Update.Inc(x => x.Quantity, -amount);


        return await collection.FindOneAndUpdateAsync(
            filter, updateDefinition);
        ;
    }
}