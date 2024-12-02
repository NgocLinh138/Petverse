namespace Domain.Abstractions.Repositories.Base;

public interface IAuditRepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>
    where TEntity : class
{
    void SoftDelete(TEntity entity);
    void SoftDeleteMultiple(List<TEntity> entities);
}
