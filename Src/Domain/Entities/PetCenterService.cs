using Contract.Enumerations;
using Contract.Services.V1.PetCenterService;
using Domain.Abstractions.EntityBase;

namespace Domain.Entities;

public class PetCenterService : EntityBase<int>
{
    public Guid PetCenterId { get; set; }
    public int PetServiceId { get; set; }
    public decimal Price { get; set; }
    public float? Rate { get; set; }
    public string? Description { get; set; }
    public ServiceType Type { get; set; }
    public int? Capacity { get; set; }
    public string? Schedule { get; set; }

    public virtual PetCenter PetCenter { get; set; } = null!;
    public virtual PetService PetService { get; set; } = null!;
    public virtual ICollection<ServiceAppointment> ServiceAppointments { get; set; } = null!;

    public void Update(Command.UpdatePetCenterServiceCommand request)
    {
        if (request.Price.HasValue)
            Price = request.Price.Value;

        if (!string.IsNullOrWhiteSpace(request.Description))
            Description = request.Description;

        if (request.Type.HasValue)
            Type = request.Type.Value;

        if (request.Schedule != null && request.Schedule.Any())
            Schedule = string.Join(";", request.Schedule.Select(s => $"{s.Time}-{s.Description}"));
    }

}
