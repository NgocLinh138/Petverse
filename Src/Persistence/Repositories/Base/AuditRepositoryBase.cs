using Contract.Constants;
using Domain.Abstractions.EntityBase;
using Domain.Abstractions.Repositories.Base;

namespace Persistence.Repositories.Base;

public class AuditRepositoryBase<TEntity, TKey> : RepositoryBase<TEntity, TKey>, IAuditRepositoryBase<TEntity, TKey>, IDisposable
    where TEntity : AuditEntityBase<TKey>
{
    protected AuditRepositoryBase(ApplicationDbContext context) : base(context)
    {
    }

    public void SoftDelete(TEntity entity)
    {
        entity.CreatedDate = TimeZones.GetSoutheastAsiaTime();
        Update(entity);
    }

    public void SoftDeleteMultiple(List<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedDate = TimeZones.GetSoutheastAsiaTime();
        }
        UpdateMultiple(entities);
    }
}
