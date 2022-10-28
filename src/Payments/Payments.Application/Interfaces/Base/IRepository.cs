using System.Linq.Expressions;
using Payments.Domain.Shared;

namespace Payments.Application.Interfaces.Base;

public interface IRepository
{
    Task Submit();
}

public interface IRepository<TEntity, TKey> : IRepository
    where TEntity : Entity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task AddRangeAsync(List<TEntity> entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllWhereAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> GetByIdAsync(TKey id);
    Task DeleteWhereAsync(Func<TEntity, bool> predicate);
}