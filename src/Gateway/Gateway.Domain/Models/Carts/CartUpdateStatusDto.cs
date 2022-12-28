namespace Gateway.Domain.Models.Carts;

[Serializable]
public record CartUpdateStatusDto(bool hasErrors, CartDetails cartDetails, List<CartActionError> errors)
{
    public override string ToString()
    {
        return $"{{ hasErrors = {hasErrors}, cartDetails = {cartDetails}, errors = {errors} }}";
    }
}