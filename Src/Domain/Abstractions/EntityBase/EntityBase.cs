namespace Domain.Abstractions.EntityBase;
public abstract class EntityBase<T> : IEntityBase<T>
{
    public virtual T Id { get; set; }

    public bool IsTransient()
        => Id.Equals(default(T));
}
