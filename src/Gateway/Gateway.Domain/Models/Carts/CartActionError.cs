using System.Text.Json.Serialization;

namespace Gateway.Domain.Models.Carts;

[Serializable]
public record CartActionError(Exception? attachedException,
    CartActionErrorDetailsAbstract cartActionErrorDetails);

[Serializable]
[JsonDerivedType(typeof(CartActionInsufficientStocksDetailsDto), "InsufficientStocksDetailsDto")]
[JsonDerivedType(typeof(CartActionProductDoesntExistErrorDetailsDto), "ProductDoesntExistErrorDetailsDto")]
public abstract record CartActionErrorDetailsAbstract
{
}