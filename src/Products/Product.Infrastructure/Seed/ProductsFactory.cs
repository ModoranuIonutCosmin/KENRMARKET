using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Infrastructure.Seed;

public class ProductsFactory : IProductsFactory
{
    public List<Product> CreateProducts()
    {
        return new List<Product>
        {
            new()
            {
                Id = new Guid("167f928d-1d55-4ee7-bd7a-3b77225e6ce8"),
                Name = "Carcasa AQIRYS Rigel ARGB",
                AddedDate = DateTime.Now,
                Category = CreateCategories()[2],
                Description = "Carcasa 499",
                Price = 499m,
                Quantity = 10m,
                Discount = 80,
                PhotoName = "dummy.png",
                Specifications = new Specifications
                {
                    SpecificationMetadataGroups = new List<SpecificationsMetadataGroup>
                    {
                        new()
                        {
                            Title = "Dimensiuni",
                            Specifications = new List<SpecificationMetadataLine>
                            {
                                new()
                                {
                                    Attribute = "Lungime",
                                    Value = "80cm"
                                },
                                new()
                                {
                                    Attribute = "Latime",
                                    Value = "30cm"
                                },
                                new()
                                {
                                    Attribute = "Inaltime",
                                    Value = "120cm"
                                }
                            }
                        },
                        new()
                        {
                            Title = "General",
                            Specifications = new List<SpecificationMetadataLine>
                            {
                                new()
                                {
                                    Attribute = "Tip carcasa",
                                    Value = "MiniTower"
                                },
                                new()
                                {
                                    Attribute = "Pozitionare Sursa",
                                    Value = "Jos"
                                },
                                new()
                                {
                                    Attribute = "Greutate",
                                    Value = "5kg"
                                }
                            }
                        }
                    }
                },
                Tags = new List<string>
                {
                    "resigilate"
                }
            },
            new()
            {
                Id = new Guid("5c199475-ceaa-46db-9ea6-0a8da8f661bf"),
                Name = "Carcasa AQIRYS Alt model ARGB",
                AddedDate = DateTime.Now,
                Category = CreateCategories()[2],
                Description = "Carcasa 449",
                Price = 449m,
                Quantity = 8m,
                Discount = 80,
                PhotoName = "dummy.png",
                Specifications = new Specifications
                {
                    SpecificationMetadataGroups = new List<SpecificationsMetadataGroup>
                    {
                        new()
                        {
                            Title = "Dimensiuni",
                            Specifications = new List<SpecificationMetadataLine>
                            {
                                new()
                                {
                                    Attribute = "Lungime",
                                    Value = "80cm"
                                },
                                new()
                                {
                                    Attribute = "Latime",
                                    Value = "30cm"
                                },
                                new()
                                {
                                    Attribute = "Inaltime",
                                    Value = "120cm"
                                }
                            }
                        },
                        new()
                        {
                            Title = "General",
                            Specifications = new List<SpecificationMetadataLine>
                            {
                                new()
                                {
                                    Attribute = "Tip carcasa",
                                    Value = "MiniTower"
                                },
                                new()
                                {
                                    Attribute = "Pozitionare Sursa",
                                    Value = "Jos"
                                },
                                new()
                                {
                                    Attribute = "Greutate",
                                    Value = "5kg"
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    public List<Category> CreateCategories()
    {
        var itCategory = new Category
        {
            Id = Guid.Parse("1b14ffef-2343-451d-a1e4-17e0255aa4e9"),
            Name = "IT"
        };

        var networkGearCategory = new Category
        {
            Id = Guid.Parse("75ce7fe7-fe0b-45bd-b25c-a59f26d136a0"),
            Name = "Network Gear"
        };

        var pcPartsCategory = new Category
        {
            Id = Guid.Parse("768ebbf3-f2c4-468b-a794-4ade123e2f02"),
            Name = "PC Parts"
        };

        itCategory.Children = new List<Category> { pcPartsCategory, networkGearCategory };

        var pcCasesCategory = new Category
        {
            Id = Guid.Parse("25e947ea-546c-47f5-afe4-c9693b33561c"),
            Name = "PC Cases"
        };

        pcPartsCategory.Children = new List<Category> { pcCasesCategory };

        return new List<Category> { itCategory, networkGearCategory, pcPartsCategory, pcCasesCategory };
    }
}