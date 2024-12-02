namespace Domain.Entities.JunctionEntity;

public class SpeciesPlace
{
    public int PlaceId { get; set; }
    public int SpeciesId { get; set; }

    public virtual Place Place { get; set; }
    public virtual Species Species { get; set; }
}
