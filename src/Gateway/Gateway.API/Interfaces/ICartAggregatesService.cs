using Gateway.API.Auth.Models.Carts;
using Gateway.API.Models;

namespace Gateway.API.Interfaces
{
    public interface ICartAggregatesService
    {
        Task<(bool IsSuccess, dynamic FullCartDetails)> GetFullCartDetails(Guid customerId);
        Task<(bool IsSuccess, dynamic FullCartDetails)> ModifyCart(Guid customerId, UpdateCartRequestDTO newCartContents);
    }
}
