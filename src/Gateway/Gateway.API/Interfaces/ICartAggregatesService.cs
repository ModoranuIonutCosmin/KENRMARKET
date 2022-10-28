using Gateway.Domain.Models.Carts;

namespace Gateway.API.Interfaces;

public interface ICartAggregatesService
{
    Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(Guid customerId);
    Task<(bool IsSuccess, dynamic FullCartDetails)> ModifyCart(Guid customerId, UpdateCartRequestDTO newCartContents);
    Task<(bool IsSuccess, dynamic CartDetails)> AddItemToCart(Guid customerId, CartItemDTO cartItemDto);
}