using Products.Domain.Entities;
using Products.Domain.Models;

namespace Products.Infrastructure.Seed
{
    public class ProductsFactory : IProductsFactory
    {
        public List<Product> CreateProducts()
        {
            return new List<Product>()
            {
                new()
                {
                    Name = "Carcasa AQIRYS Rigel ARGB",
                    AddedDate = DateTime.Now,
                    Category = new Category()
                    {
                        Name = "IT"
                    },
                    Description = "Carcasa 499",
                    Price = 499m,
                    Quantity = 10m,
                    Discount = 80,
                    PhotoName = "dummy.png",
                    Specifications = new Domain.Models.Specifications()
                    {
                        SpecificationMetadataGroups = new List<SpecificationsMetadataGroup>()
                        {
                            new SpecificationsMetadataGroup()
                            {
                                Title = "Dimensiuni",
                                Specifications = new List<SpecificationMetadataLine>()
                                {
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Lungime",
                                        Value = "80cm",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Latime",
                                        Value = "30cm",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Inaltime",
                                        Value = "120cm",
                                    },

                                }
                            },
                            new SpecificationsMetadataGroup()
                            {
                                Title = "General",
                                Specifications = new List<SpecificationMetadataLine>()
                                {
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Tip carcasa",
                                        Value = "MiniTower",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Pozitionare Sursa",
                                        Value = "Jos",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Greutate",
                                        Value = "5kg",
                                    },

                                }
                            },
                        }
                    },
                    Tags = new List<string>()
                    {
                        "resigilate",
                    }
                },
                new()
                {
                    Name = "Carcasa AQIRYS Alt model ARGB",
                    AddedDate = DateTime.Now,
                    Category = new Category()
                    {
                        Name = "IT"
                    },
                    Description = "Carcasa 449",
                    Price = 449m,
                    Quantity = 8m,
                    Discount = 80,
                    PhotoName = "dummy.png",
                    Specifications = new Domain.Models.Specifications()
                    {
                        SpecificationMetadataGroups = new List<SpecificationsMetadataGroup>()
                        {
                            new SpecificationsMetadataGroup()
                            {
                                Title = "Dimensiuni",
                                Specifications = new List<SpecificationMetadataLine>()
                                {
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Lungime",
                                        Value = "80cm",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Latime",
                                        Value = "30cm",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Inaltime",
                                        Value = "120cm",
                                    },

                                }
                            },
                            new SpecificationsMetadataGroup()
                            {
                                Title = "General",
                                Specifications = new List<SpecificationMetadataLine>()
                                {
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Tip carcasa",
                                        Value = "MiniTower",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Pozitionare Sursa",
                                        Value = "Jos",
                                    },
                                    new SpecificationMetadataLine()
                                    {
                                        Attribute = "Greutate",
                                        Value = "5kg",
                                    },

                                }
                            },
                        }
                    }
                }
            };
        }

        public List<Category> CreateCategories()
        {
            var itCategory = new Category()
            {
                Name = "IT",
                CategoryParent = null
            };
            var pcPartsCategory = new Category()
            {
                CategoryParent = itCategory,
                Name = "PC Parts"
            };

            return new List<Category>()
            {
                itCategory,
                pcPartsCategory,
                new Category()
                {
                    Name = "PC Cases",
                    CategoryParent = pcPartsCategory
                }
            };
        }
    }
}

