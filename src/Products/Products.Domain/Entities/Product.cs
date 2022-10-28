using Products.Domain.Models;
using Products.Domain.Shared;

namespace Products.Domain.Entities;

public class Product : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
    public string PhotoName { get; set; }
    public decimal Discount { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public Category Category { get; set; }
    public DateTimeOffset AddedDate { get; set; }
    public Specifications Specifications { get; set; }
    public List<string> Tags { get; set; }
}