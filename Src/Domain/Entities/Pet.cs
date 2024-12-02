using Contract.Constants;
using Contract.Enumerations;
using Contract.JsonConverters;
using Contract.Services.V1.Pet;
using Domain.Abstractions.EntityBase;
using Domain.Entities.Identity;
using Domain.Entities.JunctionEntity;

namespace Domain.Entities;

public class Pet : AuditEntityBase<int>
{
    public Guid UserId { get; set; }
    public int BreedId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public float Weight { get; set; }
    public bool Sterilized { get; set; }
    public string Avatar { get; set; } = null!;
    public string? Description { get; set; } = null;

    public virtual AppUser User { get; set; } = null!;
    public virtual Breed Breed { get; set; } = null!;


    public virtual ICollection<PetImage> Photos { get; set; } = null!;
    public virtual ICollection<PetVaccinated> PetVaccinateds { get; set; } = null!;
    public virtual ICollection<VaccineRecommendation> VaccineRecommendations { get; set; } = null!;
    public virtual ICollection<Appointment> Appointments { get; set; } = null!;

    public void Update(Command.UpdatePetCommand request)
    {
        if (!string.IsNullOrEmpty(request.Name))
            Name = request.Name;

        if (!string.IsNullOrEmpty(request.BirthDate))
            BirthDate = DateTimeConverters.StringToDate(request.BirthDate).Value;

        if (request.Gender.HasValue)
            Gender = request.Gender.Value;

        if (request.Weight.HasValue)
            Weight = request.Weight.Value;

        if (request.Sterilized.HasValue)
            Sterilized = request.Sterilized.Value;

        if (!string.IsNullOrEmpty(request.Description))
            Description = request.Description;

        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
