using Contract.Constants;

namespace Domain.Abstractions.EntityBase;
public abstract class AuditEntityBase<T> : EntityBase<T>, IAudit
{
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }
}

