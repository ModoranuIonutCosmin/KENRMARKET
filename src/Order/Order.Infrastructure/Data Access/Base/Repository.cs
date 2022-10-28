using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Order.Application.Interfaces.Base;
using Order.Domain.Shared;

namespace Order.Infrastructure.Data_Access.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity
{
    private readonly ILogger<Repository<TEntity, TKey>> _logger;
    protected readonly OrdersDBContext _ordersDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public Repository(OrdersDBContext context, ILogger<Repository<TEntity, TKey>> logger,
        IUnitOfWork unitOfWork)
    {
        _ordersDbContext = context;
        _logger = logger;
        _unitOfWork = unitOfWork;

        _unitOfWork.Register(this);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _ordersDbContext.AddAsync(entity);
        await _ordersDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        await _ordersDbContext.AddRangeAsync(entities);
        await _ordersDbContext.SaveChangesAsync();
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _ordersDbContext.Remove(entity);
        await _ordersDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _ordersDbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWhereAsync
        (Expression<Func<TEntity, bool>> predicate)
    {
        return await _ordersDbContext.Set<TEntity>()
            .Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _ordersDbContext.FindAsync<TEntity>(id);
    }

    public async Task DeleteWhereAsync(Func<TEntity, bool> predicate)
    {
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        _ordersDbContext.RemoveRange(_ordersDbContext.Set<TEntity>().Where(predicate));

        await _ordersDbContext.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _ordersDbContext.Update(entity);
        await _ordersDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Submit()
    {
        await _ordersDbContext.SaveChangesAsync();
    }
}