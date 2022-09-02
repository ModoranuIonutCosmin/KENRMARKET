using Gateway.API.Models;

namespace Gateway.API.Interfaces;

public interface IProductsService
{
    Task<(bool IsOk, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
}