using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.CenterBreed;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class CenterBreed : EntityBase<int>, IDateTracking
{
    public Guid PetCenterId { get; set; }
    public int SpeciesId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public float? Rate { get; set; }
    public CenterBreedStatus Status { get; set; }
    public string? CancelReason { get; set; }
    public bool IsDisabled { get; set; } 


    public virtual PetCenter PetCenter { get; set; }
    public virtual Species Species { get; set; }
    public virtual ICollection<BreedAppointment> BreedAppointments { get; set; }
    public virtual ICollection<CenterBreedImage> CenterBreedImages { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public void Update(Command.UpdateCenterBreedCommand request)
    {
        Status = request.Status;
        CancelReason = request.CancelReason;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }

    public void UpdateAvailability(Command.UpdateCenterBreedAvailabilityCommand request)
    {
        IsDisabled = request.IsDisabled;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
