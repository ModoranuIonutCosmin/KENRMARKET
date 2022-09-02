using Gateway.API.Models;

namespace Gateway.API.Interfaces;

public interface ICartService
{
    Task<(bool IsOk, CartDetails CartDetails, string ErrorMessage)> GetCartDetails(string customerId);
}