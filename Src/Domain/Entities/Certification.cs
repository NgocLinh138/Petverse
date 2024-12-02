using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class Certification : EntityBase<int>
{
    public int ApplicationId { get; set; }
    public string Image { get; set; } = null!;

    public virtual Application Application { get; set; } = null!;
}
