﻿namespace Gateway.Domain.Models.Products;

public class Category
{
    public string   Id             { get; set; }
    public string   Name           { get; set; }
    public Category CategoryParent { get; set; }
}