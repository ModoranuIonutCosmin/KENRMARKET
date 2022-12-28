namespace Gateway.Domain.Models.Carts;

public record CartDetailsFullDto(CartDetailsDTO CartDetails)
{
    public override string ToString()
    {
        return $"{{ CartDetails = {CartDetails} }}";
    }
}