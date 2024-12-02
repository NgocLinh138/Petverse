using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class CenterBreedImage : EntityBase<int>
{
    public int CenterBreedId { get; set; }
    public string Image { get; set; } = null!;

    public virtual CenterBreed CenterBreed { get; set; } = null!;
}
