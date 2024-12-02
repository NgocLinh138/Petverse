using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class Breed : EntityBase<int>
{
    public int SpeciesId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual Species Species { get; set; } = null!;
    public virtual ICollection<Pet> Pets { get; set; } = null!;

}
