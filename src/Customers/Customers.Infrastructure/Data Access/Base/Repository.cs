using System.Linq.Expressions;
using Customers.Application.Interfaces;
using Customers.Application.Interfaces.Base;
using Customers.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Customers.Infrastructure.Data_Access.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity
{
    private readonly   ILogger<Repository<TEntity, TKey>> _logger;
    protected readonly CustomersDBContext                    _customersDbContext;
    private readonly   IUnitOfWork                        _unitOfWork;

    public Repository(CustomersDBContext context, ILogger<Repository<TEntity, TKey>> logger,
        IUnitOfWork unitOfWork)
    {
        _customersDbContext = context;
        _logger             = logger;
        _unitOfWork         = unitOfWork;

        _unitOfWork.Register(this);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        await _customersDbContext.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        if (entities == null)
        {
            throw new ArgumentNullException(nameof(entities));
        }

        await _customersDbContext.AddRangeAsync(entities);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _customersDbContext.Remove(entity);
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _customersDbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWhereAsync
        (Expression<Func<TEntity, bool>> predicate)
    {
        return await _customersDbContext.Set<TEntity>()
                                        .Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _customersDbContext.FindAsync<TEntity>(id);
    }

    public async Task DeleteWhereAsync(Func<TEntity, bool> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        _customersDbContext.RemoveRange(_customersDbContext.Set<TEntity>().Where(predicate));

    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _customersDbContext.Update(entity);
        return entity;
    }

    public async Task Submit()
    {
        await _customersDbContext.SaveChangesAsync();
    }
}