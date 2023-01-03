using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
using Products.Application.Interfaces.Base;
using Products.Domain.Shared;

namespace Products.Infrastructure.Data_Access.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity
{
    private readonly   ILogger<Repository<TEntity, TKey>> _logger;
    protected readonly ProductsDbContext                  _productsDbContext;

    public Repository(ProductsDbContext context, ILogger<Repository<TEntity, TKey>> logger)
    {
        _productsDbContext = context;
        _logger            = logger;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        throw new NotImplementedException();
        return entity;
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TEntity>> GetAllWhereAsync
        (Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteWhereAsync(Func<TEntity, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

}