using Cart.Domain.Entities;

namespace Cart.Infrastructure.Interfaces;

public interface ICartDetailsFactory
{
    Task<List<CartDetails>> SeedCartDetails();
}