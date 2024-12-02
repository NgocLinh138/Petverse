namespace Domain.Entities.JunctionEntity;

public class SpeciesJob
{
    public int SpeciesId { get; set; }
    public Guid JobId { get; set; }

    public virtual Species Species { get; set; } = null!;
    public virtual Job Job { get; set; } = null!;
}
