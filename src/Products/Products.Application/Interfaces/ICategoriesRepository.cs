using Products.Application.Interfaces.Base;
using Products.Domain.Entities;

namespace Products.Application.Interfaces;

public interface ICategoriesRepository : IRepository<Category, Guid>
{
    public Task<Category> GetCategoryByName(string categoryName);
}