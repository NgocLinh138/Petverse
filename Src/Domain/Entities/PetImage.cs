using Contract.Constants;
using Contract.Enumerations;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class PetImage : EntityBase<int>, IDateTracking
{
    public int PetId { get; set; }
    public MediaType Type { get; set; }
    public string URL { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public virtual Pet Pet { get; set; }
}
