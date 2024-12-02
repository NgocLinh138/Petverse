namespace Domain.Abstractions.EntityBase;
public interface ISoftDelete
{
    DateTime? DeletedDate { get; set; }
    bool IsDeleted { get; set; }
    void Undo()
    {
        DeletedDate = null;
    }
}
