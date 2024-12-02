using Contract.Constants;
using Contract.Enumerations;
using Contract.JsonConverters;
using Contract.Services.V1.User;
using Domain.Abstractions.EntityBase;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity;
public class AppUser : IdentityUser<Guid>, IAudit
{
    public Guid RoleId { get; set; }
    public string FullName { get; set; } = null!;
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Avatar { get; set; } = null!;
    public string? Address { get; set; } = null!;
    public decimal Balance { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = TimeZones.GetSoutheastAsiaTime();
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsDeleted { get; set; }

    public virtual AppRole Role { get; set; } = null!;
    public virtual Application Application { get; set; } = null!;

    public virtual ICollection<Transaction> Payments { get; set; } = null!;
    public virtual ICollection<Pet> Pets { get; set; } = null!;

    //============================== Appointment ==============================
    public virtual ICollection<Appointment> Appointments { get; set; } = null!;
    public virtual ICollection<BreedAppointment> BreedAppointments { get; set; } = null!;
    public virtual ICollection<ServiceAppointment> ServiceAppointments { get; set; } = null!;
    public void Update(Command.UpdateUserCommand request, string? newBlobPath)
    {
        FullName = string.IsNullOrEmpty(request.FullName) ? FullName : request.FullName;
        Gender = request.Gender.HasValue ? request.Gender : Gender;
        var DOBUpdate = DateTimeConverters.StringToDate(request.DateOfBirth);
        if (DOBUpdate.HasValue)
            DateOfBirth = DOBUpdate.Value;
        Avatar = string.IsNullOrEmpty(newBlobPath) ? Avatar : newBlobPath;
        Address = string.IsNullOrEmpty(request.Address) ? Address : request.Address;
        UpdatedDate = TimeZones.GetSoutheastAsiaTime();
        PhoneNumber = string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber : request.PhoneNumber;
    }
}
