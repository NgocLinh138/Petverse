using Contract.Services.V1.PetService;
using Domain.Abstractions.EntityBase;
using Domain.Entities.Junction;

namespace Domain.Entities;

public class PetService : EntityBase<int>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<ApplicationPetService> ApplicationPetServices { get; set; } = null!;
    public virtual ICollection<PetCenterService> PetCenterServices { get; set; } = null!;

    public void Update(Command.UpdatePetServiceCommand request)
    {
        Name = request.Name;
        Description = request.Description;
    }
}
