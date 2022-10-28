using Gateway.Domain.Models.Carts;

namespace Gateway.API.Interfaces;

public interface ICartService
{
    Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(Guid customerId);

    Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> UpdateCart(Guid customerId,
        CartDetails cartDetails);
}