using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Application;
using Domain.Abstractions.EntityBase;
using Domain.Entities.Identity;
using Domain.Entities.Junction;
using Microsoft.IdentityModel.Tokens;
using static Contract.Services.V1.PetCenter.Command;
namespace Domain.Entities;

public class Application : EntityBase<int>, IDateTracking
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? BankName { get; set; } = null!;
    public string? BankNumber { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Avatar { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Yoe { get; set; }
    public string? CancelReason { get; set; }
    public JobApplicationStatus Status { get; set; }
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }

    public virtual AppUser User { get; set; } = null!;
    public virtual PetCenter PetCenter { get; set; } = null!;
    public virtual ICollection<ApplicationPetService> ApplicationPetServices { get; set; } = null!;
    public virtual ICollection<Certification> Certifications { get; set; } = null!;



    public void Create(Command.CreateApplicationCommand request, string? newBlobPath)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        UserId = request.UserId;
        Name = request.Name ?? throw new ArgumentException("FullName is required.");
        PhoneNumber = request.PhoneNumber ?? throw new ArgumentException("PhoneNumber is required.");
        Address = request.Address ?? throw new ArgumentException("Address is required.");
        Avatar = string.IsNullOrEmpty(newBlobPath) ? Avatar : newBlobPath;
        Description = request.Description ?? throw new ArgumentException("Description is required.");
        Status = JobApplicationStatus.Processing;
        ApplicationPetServices ??= new List<ApplicationPetService>();
        AddPetServices(request.PetServiceIds);
        CreatedDate = TimeZones.GetSoutheastAsiaTime();
    }

    public void Update(UpdatePetCenterCommand request, string NewImage)
    {
        Name = string.IsNullOrEmpty(request.Name) ? Name : request.Name;
        Address = string.IsNullOrEmpty(request.Address) ? Address : request.Address;
        PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? PhoneNumber : request.PhoneNumber;
        Avatar = NewImage.IsNullOrEmpty() ? Avatar : NewImage;
        Description = string.IsNullOrEmpty(request.Description) ? Description : request.Description;
        BankName = string.IsNullOrEmpty(request.BankName) ? BankName : request.BankName;
        BankNumber = string.IsNullOrEmpty(request.BankNumber) ? BankNumber : request.BankNumber;
    }

    private void AddPetServices(IEnumerable<int>? petServiceIds)
    {
        if (petServiceIds == null) return;

        foreach (var serviceId in petServiceIds)
        {
            if (!ApplicationPetServices.Any(aps => aps.PetServiceId == serviceId))
            {
                ApplicationPetServices.Add(new ApplicationPetService
                {
                    PetServiceId = serviceId
                });
            }
        }
    }
}
