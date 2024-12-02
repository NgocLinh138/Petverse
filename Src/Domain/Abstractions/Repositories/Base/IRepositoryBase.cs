using System.Linq.Expressions;

namespace Domain.Abstractions.Repositories.Base;

public interface IRepositoryBase<TEntity, in TKey> where TEntity : class
{
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

    Task AddAsync(TEntity entity);
    Task AddMultipleAsync(List<TEntity> entities);

    void Update(TEntity entity);
    public void UpdateMultiple(List<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveMultiple(List<TEntity> entities);
}
