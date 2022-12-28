namespace Gateway.Domain.Models.Products;

public class SpecificationsMetadataGroup
{
    public string                          Title          { get; set; }
    public List<SpecificationMetadataLine> Specifications { get; set; }
}