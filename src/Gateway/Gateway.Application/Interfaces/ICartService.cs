using Gateway.API.Models;
using Gateway.Domain.Models.Carts;
using Gateway.Domain.Models.Orders;

namespace Gateway.API.Interfaces;

public interface ICartService
{
    Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(Guid customerId);

    Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> UpdateCart(Guid customerId,
        CartDetails cartDetails);

    Task<(bool IsOk, CartDetailsDTO CartDetails, string ErrorMessage)> CheckoutCart(Guid customerId,
        Address shippingAddress);
}