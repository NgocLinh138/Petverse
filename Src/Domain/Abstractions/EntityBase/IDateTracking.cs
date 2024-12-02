namespace Domain.Abstractions.EntityBase;
public interface IDateTracking
{
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }

}
