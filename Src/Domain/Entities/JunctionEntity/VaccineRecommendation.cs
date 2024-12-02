using Domain.Abstractions.EntityBase;

namespace Domain.Entities.JunctionEntity;

public class VaccineRecommendation : EntityBase<int>
{
    public int VaccineId { get; set; }
    public int PetId { get; set; }

    public virtual Vaccine Vaccine { get; set; }
    public virtual Pet Pet { get; set; }
}
