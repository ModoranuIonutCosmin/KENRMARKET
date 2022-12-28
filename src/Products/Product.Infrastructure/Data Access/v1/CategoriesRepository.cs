using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;
using Products.Infrastructure.Data_Access.Base;

namespace Products.Infrastructure.Data_Access.v1;

//TODO: Schimbat la generic repository cu UoW
public class CategoriesRepository : Repository<Category, Guid>, ICategoriesRepository
{
    private readonly ProductsDbContext _productsDbContext;

    public CategoriesRepository(ProductsDbContext productsDbContext, ILogger<CategoriesRepository> logger)
        : base(productsDbContext, logger)
    {
        _productsDbContext = productsDbContext;
    }

    public async Task<Category> GetCategoryByName(string categoryName)
    {
        return (await _productsDbContext.Categories
                                        .FindAsync(Builders<Category>.Filter.Eq(c => c.Name, categoryName))
            ).FirstOrDefault();
    }
}