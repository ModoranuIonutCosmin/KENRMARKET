namespace Gateway.Domain.Models.Carts;

[Serializable]
public record CartActionInsufficientStocksDetailsDto
    (Guid productId, decimal availableStock, decimal requestedStock) : CartActionErrorDetailsAbstract;
