using Microsoft.AspNetCore.Mvc;
using Products.Application.Interfaces.Services;
using Products.Domain.Models;

namespace Products.API.Controllers;

[ApiVersion("1.0")]
public class ProductsController : BaseController
{
    private readonly IProductsService productsService;

    public ProductsController(IProductsService productsService)
    {
        this.productsService = productsService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await productsService.GetProducts());
    }

    [HttpPost("filtered")]
    public async Task<IActionResult> GetAllProducts([FromBody] FilterOptions filterOptions)
    {
        return Ok(await productsService.GetProductsFiltered(filterOptions));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleProduct([FromRoute] Guid id)
    {
        var product = await productsService.GetProduct(id);

        if (product == null) return NotFound();

        return Ok(
            product
        );
    }
}