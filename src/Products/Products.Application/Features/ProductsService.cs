using Products.Application.Interfaces;
using Products.Application.Interfaces.Services;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Features;

public class ProductsService : IProductsService
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IProductsRepository _productsRepository;

    public ProductsService(IProductsRepository productsRepository,
        ICategoriesRepository categoriesRepository)
    {
        _productsRepository = productsRepository;
        _categoriesRepository = categoriesRepository;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _productsRepository.GetAllProducts();
    }

    public async Task<Product> GetProduct(Guid productId)
    {
        return await _productsRepository.GetProduct(productId);
    }

    public async Task<List<Product>> GetProductsFiltered(FilterOptions filterOptions)
    {
        var rootCategory = await _categoriesRepository.GetCategoryByName(filterOptions.CategoryName);

        if (rootCategory == null) return new List<Product>();

        return await _productsRepository.FilterProducts(filterOptions);
    }


    private List<Category> RecurseCategories(Category category)
    {
        var results = new List<Category> { category };

        if (!category.Children.Any()) return results;

        foreach (var child in category.Children) results.AddRange(RecurseCategories(child));

        return results;
    }
}