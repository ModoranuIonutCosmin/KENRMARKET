using MongoDB.Bson;
using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Domain.Models;
using Products.Infrastructure.Seed;

namespace Products.Infrastructure.Data_Access.v1;

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

    public async Task<Product> GetProduct(Guid id)
    {
        return await dbContext.Products
            .Find(Builders<Product>.Filter.Eq("_id", id))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await (await dbContext.Products.FindAsync(new BsonDocument()))
            .ToListAsync();

        return products;
    }

    public async Task<List<Product>> GetAllProductsWithIdsInList(List<Guid> productIds)
    {
        //TODO: Test
        var productsCollection = dbContext.Products;
        
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


        // if (filterOptions.SpecificationsFilters.Count() > 0)
        // {
        //     foreach (var specification in filterOptions.SpecificationsFilters)
        //     {
        //         // var specificationFilter = builder.
        //     }
        // }


        return (await dbContext.Products.FindAsync(filter)).ToList();
    }
}