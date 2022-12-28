using Microsoft.AspNetCore.Mvc;
using Products.Application.Interfaces.Services;
using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.API.Controllers;

[ApiVersion("1.0")]
public class ProductsController : BaseController
{
    private readonly ILogger<ProductsController> _logger;
    private readonly IProductsService            productsService;

    public ProductsController(IProductsService productsService, ILogger<ProductsController> logger)
    {
        this.productsService = productsService;
        _logger              = logger;
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Product>))]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await productsService.GetProducts();

        _logger.LogInformation("Attempting to get all products");

        if (!products.Any())
        {
            _logger.LogInformation("Found nothing");

            return NotFound();
        }

        _logger.LogInformation("Got all products, count={@count}", products.Count);

        return Ok(products);
    }

    [HttpPost("filtered")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(List<Product>))]
    public async Task<IActionResult> GetProductsFiltered([FromBody] FilterOptions filterOptions)
    {
        var filteredProducts = await productsService.GetProductsFiltered(filterOptions);

        _logger.LogInformation("Attempting to get all filtered products");

        if (!filteredProducts.Any())
        {
            _logger.LogInformation("Found nothing. ");

            return NotFound();
        }

        _logger.LogInformation("Got all filtered products, count={@count}", filteredProducts.Count);

        return Ok(filteredProducts);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK, type: typeof(Product))]
    public async Task<IActionResult> GetSingleProduct([FromRoute] Guid id)
    {
        _logger.LogInformation("Attempting to get productId={@id}", id);

        var product = await productsService.GetProduct(id);

        if (product == null)
        {
            _logger.LogInformation("Found nothing for productId={@id}", id);

            return NotFound("No products found.");
        }

        _logger.LogInformation("Found product id={@id}, productName={@name}", id, product.Name);

        return Ok(
                  product
                 );
    }
}