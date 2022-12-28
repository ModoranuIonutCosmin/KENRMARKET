using Products.Domain.Shared;

namespace Products.Domain.Entities;

public class Category : Entity, IAggregateRoot
{
    public string         Name     { get; set; }
    public List<Category> Children { get; set; } = new();
}