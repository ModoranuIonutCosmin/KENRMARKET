﻿namespace Gateway.Domain.Models.Products;

public class SpecificationMetadataLine
{
    public string Attribute { get; set; }
    public string Value     { get; set; }
    public int    Priority  { get; set; }
}