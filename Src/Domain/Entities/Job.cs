using Contract.Constants;
using Contract.Services.V1.Job;
using Domain.Abstractions.EntityBase;
using Domain.Entities.JunctionEntity;

namespace Domain.Entities;

public class Job : EntityBase<Guid>, IDateTracking
{
    public Guid PetCenterId { get; set; }
    public string Description { get; set; }
    public bool HavePhoto { get; set; }
    public bool HaveCamera { get; set; }
    public bool HaveTransport { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public virtual PetCenter PetCenter { get; set; } = null!;
    public virtual ICollection<SpeciesJob> SpeciesJobs { get; set; } = null!;

    public static Job Create(Command.CreateJobCommand request) => new()
    {
        PetCenterId = request.PetCenterId,
        Description = request.Description,
        HavePhoto = request.HavePhoto,
        HaveCamera = request.HaveCamera,
        HaveTransport = request.HaveTransport,
    };
    public void Update(Command.UpdateJobCommand request)
    {
        Description = request.Description;
        HaveCamera = request.HaveCamera;
        HavePhoto = request.HavePhoto;
        HaveTransport = request.HaveTransport;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
    }
}
