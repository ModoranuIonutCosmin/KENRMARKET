namespace Gateway.Domain.Models.Carts;

public record CartActionProductDoesntExistErrorDetailsDto(Guid productId) : CartActionErrorDetailsAbstract;