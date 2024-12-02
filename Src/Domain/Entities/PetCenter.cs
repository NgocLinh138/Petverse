using Contract.Constants;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class PetCenter : AuditEntityBase<Guid>
{
    public int ApplicationId { get; set; }
    public int NumPet { get; set; }
    public bool IsVerified { get; set; }
    public int NumReported { get; set; } = 0;
    public bool IsDisabled { get; set; } = false;

    public virtual Job Job { get; set; } = null!;
    public virtual Application Application { get; set; } = null!;
    public virtual ICollection<CenterBreed> CenterBreeds { get; set; } = null!;
    public virtual ICollection<PetCenterService> PetCenterServices { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; } = null!;
    public void Update(Command.UpdatePetCenterCommand request)
    {
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
