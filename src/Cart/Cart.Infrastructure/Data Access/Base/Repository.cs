using System.Linq.Expressions;
using Cart.Application.Interfaces;
using Cart.Application.Interfaces.Base;
using Cart.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cart.Infrastructure.Data_Access.Base;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : Entity
{
    private readonly ILogger<Repository<TEntity, TKey>> _logger;
    protected readonly CartsDBContext _cartsDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public Repository(CartsDBContext context, ILogger<Repository<TEntity, TKey>> logger,
        IUnitOfWork unitOfWork)
    {
        _cartsDbContext = context;
        _logger = logger;
        _unitOfWork = unitOfWork;

        _unitOfWork.Register(this);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        await _cartsDbContext.AddAsync(entity);
        await _cartsDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        if (entities == null) throw new ArgumentNullException(nameof(entities));

        await _cartsDbContext.AddRangeAsync(entities);
        await _cartsDbContext.SaveChangesAsync();
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _cartsDbContext.Remove(entity);
        await _cartsDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _cartsDbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllWhereAsync
        (Expression<Func<TEntity, bool>> predicate)
    {
        return await _cartsDbContext.Set<TEntity>()
            .Where(predicate).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey id)
    {
        return await _cartsDbContext.FindAsync<TEntity>(id);
    }

    public async Task DeleteWhereAsync(Func<TEntity, bool> predicate)
    {
        if (predicate == null)
            throw new ArgumentNullException(nameof(predicate));

        _cartsDbContext.RemoveRange(_cartsDbContext.Set<TEntity>().Where(predicate));

        await _cartsDbContext.SaveChangesAsync();
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _cartsDbContext.Update(entity);
        await _cartsDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Submit()
    {
        await _cartsDbContext.SaveChangesAsync();
    }
}