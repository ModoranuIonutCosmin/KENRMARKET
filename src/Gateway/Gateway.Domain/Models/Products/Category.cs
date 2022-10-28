namespace Gateway.Domain.Models.Products;

public class Category
{
    public string Name { get; set; }
    public List<Category> Children { get; set; } = new();
}