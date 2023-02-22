using Gateway.Domain.Models.Products;

namespace Gateway.Tests.Seed;

public class ProductsFactory
{
    //seed Product objects
    public static List<Product> SeedProducts()
    {
        return new List<Product>()
        {
            new Product()
            {
                Id = new Guid("5DD9F8B0-22A5-4CC4-85FD-ABD0424DE45B"),
                Name = "Product Name",
                Description = "Product Description",
                Price = 10.00m,
                Quantity = 10
            },
            new Product()
            {
                Id = new Guid("29B204C5-C827-477C-BFE6-F2037742C745"),
                Name = "Product Name 2",
                Description = "Product Description 2",
                Price = 20.00m,
                Quantity = 20
            },
            new Product()
            {
                Id = new Guid("B7A159A0-1BFF-4C1D-B1FE-9B4D5AAD1375"),
                Name = "Product Name 3",
                Description = "Product Description 3",
                Price = 30.00m,
                Quantity = 30
            }
        };
    }
}