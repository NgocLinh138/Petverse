using Contract.Constants;
using Contract.Enumerations;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class Tracking : EntityBase<int>
{
    public int ScheduleId { get; set; }
    public MediaType Type { get; set; }
    public string URL { get; set; }
    public DateTime UploadedAt { get; set; } = TimeZones.GetSoutheastAsiaTime();

    public virtual Schedule Schedule { get; set; }
}