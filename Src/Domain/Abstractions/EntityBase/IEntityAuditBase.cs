namespace Domain.Abstractions.EntityBase;
public interface IEntityAuditBase<TKey> : IEntityBase<TKey>, IAudit
{
}
