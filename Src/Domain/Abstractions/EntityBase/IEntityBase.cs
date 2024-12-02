namespace Domain.Abstractions.EntityBase;
public interface IEntityBase<TKey>
{
    TKey Id { get; set; }
}
