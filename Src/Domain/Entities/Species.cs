using Domain.Abstractions.EntityBase;
using Domain.Entities.JunctionEntity;

namespace Domain.Entities;

public class Species : EntityBase<int>
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Breed> Breeds { get; set; } = null!;
    public virtual ICollection<SpeciesJob> JobSpecieses { get; set; } = null!;
    public virtual ICollection<Vaccine> Vaccines { get; set; } = null!;
    public virtual ICollection<CenterBreed> CenterBreeds { get; set; } = null!;
    public virtual ICollection<SpeciesPlace> SpeciesPlaces { get; set; } = null!;
}
