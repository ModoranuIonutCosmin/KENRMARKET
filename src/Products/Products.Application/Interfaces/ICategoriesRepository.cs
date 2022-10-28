using Products.Domain.Entities;

namespace Products.Application.Interfaces;

public interface ICategoriesRepository
{
    public Task<Category> GetCategoryByName(string categoryName);
}