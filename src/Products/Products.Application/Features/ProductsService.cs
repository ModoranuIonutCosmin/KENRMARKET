using IntegrationEvents.Models;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces;
using Products.Application.Interfaces.Services;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Application.Features;

public class ProductsService : IProductsService
{
    private readonly ICategoriesRepository    _categoriesRepository;
    private readonly ILogger<ProductsService> _logger;
    private readonly IProductsRepository      _productsRepository;

    public ProductsService(IProductsRepository productsRepository,
        ICategoriesRepository categoriesRepository,
        ILogger<ProductsService> logger)
    {
        _productsRepository   = productsRepository;
        _categoriesRepository = categoriesRepository;
        _logger               = logger;
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

        var categoriesSearched = rootCategory != null
            ? RecurseCategories(rootCategory)
            : new List<Category>();

        filterOptions.Categories = categoriesSearched;

        return await _productsRepository.FilterProducts(filterOptions);
    }

    public async Task<Product> DeductStocksForProduct(Guid productId, decimal deduction)
    {
        //TODO: De schimbat din optimistic conc????

        _logger.LogInformation("Deducting price for productId={@id}, deduction={@deduction}",
                               productId, deduction);

        var result = await _productsRepository.DeductProductStock(productId, deduction);

        _logger.LogInformation("Deducted stock for productId={@productId}, quantity={@quantity}",
                               productId, deduction);

        return result;
    }

    public async Task<bool> AreProductsOnStock(List<ProductQuantity> productQuantities)
    {
        var requiredProductsIds = productQuantities
                                  .Select(p => p.ProductId).ToList();

        var requiredProducts = await _productsRepository
            .GetAllProductsWithIdsInList(requiredProductsIds);

        return requiredProducts
               .Join(productQuantities, rp => rp.Id, pq => pq.ProductId,
                     (ap, rp) =>
                     {
                         return new
                                {
                                    availableProduct = ap,
                                    requiredProduct  = rp
                                };
                     })
               .All(c => { return c.requiredProduct.Quantity <= c.availableProduct.Quantity; });
    }


    private List<Category> RecurseCategories(Category category)
    {
        var results = new List<Category> { category };

        if (!category.Children.Any())
        {
            return results;
        }

        foreach (var child in category.Children)
        {
            results.AddRange(RecurseCategories(child));
        }

        return results;
    }
}