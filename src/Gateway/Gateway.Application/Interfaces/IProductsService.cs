using Gateway.Domain.Models.Products;

namespace Gateway.Application.Interfaces;

public interface IProductsService
{
    Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    Task<(bool IsOk, Product Product, string ErrorMessage)>               GetProductByIdAsync(Guid productId);

    Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsFiltered(
        FilterOptions filterOptions);
}