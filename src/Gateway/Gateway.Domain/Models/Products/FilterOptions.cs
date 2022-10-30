using Gateway.API.Models;

namespace Gateway.Domain.Models.Products;

public class FilterOptions
{
    public decimal MinPrice { get; set; } = 0;
    public decimal MaxPrice { get; set; } = decimal.MaxValue;
    public bool UsesPriceRangeFilter { get; set; }
    public Dictionary<string, string> SpecificationsFilters { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string? CategoryName { get; set; }
}