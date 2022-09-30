using Order.Domain.Entities;

namespace Order.Infrastructure.Interfaces;

public interface ICartDetailsFactory
{
    Task<List<Domain.Entities.Order>> SeedCartDetails();
}