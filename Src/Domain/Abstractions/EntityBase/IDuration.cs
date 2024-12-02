using Contract.Constants;

namespace Domain.Abstractions.EntityBase;
public interface IDuration
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsExpired => EndDate < TimeZones.GetSoutheastAsiaTime();
}
