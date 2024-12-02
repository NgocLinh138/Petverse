using Contract.Services.V1.Vaccine;
using Domain.Abstractions.EntityBase;
using Domain.Entities.JunctionEntity;

namespace Domain.Entities;

public class Vaccine : EntityBase<int>
{
    public int SpeciesId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int MinAge { get; set; }

    public virtual Species Species { get; set; }
    public virtual ICollection<VaccineRecommendation> VaccineRecommendations { get; set; }

    public void Update(Command.UpdateVaccineCommand request)
    {
        Name = request.Name;
        Description = request.Description;
        MinAge = request.MinAge;
    }
}
