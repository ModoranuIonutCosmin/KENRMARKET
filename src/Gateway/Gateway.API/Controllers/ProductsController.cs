using Gateway.API.Interfaces;
using Gateway.Domain.Models.Products;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiVersion("1.0")]
public class ProductsController : BaseController
{
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet("products")]
    public async Task<IActionResult> Products()
    {
        var productsStatus = await _productsService.GetProductsAsync();

        if (productsStatus.IsOk) return Ok(productsStatus.Products);

        return NotFound();
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> ProductById([FromRoute] Guid productId)
    {
        var productStatus = await _productsService.GetProductByIdAsync(productId);

        if (productStatus.IsOk) return Ok(productStatus.Product);

        return NotFound();
    }

    //TODO: Documentat ca e POST ce nu creeaza resurse
    //TODO: Paginare?
    [HttpPost("filtered")]
    public async Task<IActionResult> GetAllProducts([FromBody] FilterOptions filterOptions)
    {
        
        var filteredProductsStatus = await _productsService.GetProductsFiltered(filterOptions);

        if (filteredProductsStatus.IsOk) return Ok(filteredProductsStatus.Products);

        return NotFound();
    }
}