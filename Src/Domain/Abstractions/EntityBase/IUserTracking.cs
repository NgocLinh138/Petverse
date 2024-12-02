namespace Domain.Abstractions.EntityBase;
public interface IUserTracking<TKeyCreated, TKeyUpdated>
{
    TKeyCreated CreatedBy { get; set; }
    TKeyUpdated? UpdatedBy { get; set; }
}
