using MongoDB.Driver;
using Products.Application.Interfaces;
using Products.Domain.Entities;

namespace Products.Infrastructure.Data_Access.v1;

//TODO: Schimbat la generic repository cu UoW
public class CategoriesRepository : ICategoriesRepository
{
    private readonly DbContext _dbContext;

    public CategoriesRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Category> GetCategoryByName(string categoryName)
    {
        return (await _dbContext.Categories
                .FindAsync(Builders<Category>.Filter.Eq(c => c.Name, categoryName))
            ).FirstOrDefault();
    }
}