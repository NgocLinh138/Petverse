using Contract.Enumerations;

namespace Contract.Services.V1.User;

public static class Responses
{
    public record UserResponse
    {
        public Guid Id { get; init; }
        public Guid RoleId { get; init; }
        public string Email { get; init; }
        public string FullName { get; init; }
        public string PhoneNumber { get; init; }
    }

    public record UserGetAllResponse
    {
        public Guid Id { get; init; }
        public Guid RoleId { get; init; }
        public string FullName { get; init; }
        public decimal Balance { get; init; }
        public string? Avatar { get; init; }
        public string? Address { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string CreatedDate { get; init; }
        public string? UpdatedDate { get; init; }
        public string? DeletedDate { get; init; }
        public bool IsDeleted { get; init; }
    };

    public record UserGetByIdResponse
    {
        public Guid Id { get; init; }
        public Guid? PetCenterId { get; init; }
        public Guid RoleId { get; init; }
        public string RoleName { get; init; }
        public string FullName { get; init; }
        public decimal Balance { get; init; }
        public Gender? Gender { get; init; }
        public string? DateOfBirth { get; init; }
        public string? Avatar { get; init; }
        public string? Address { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }
        public string CreatedDate { get; init; }
        public string? UpdatedDate { get; init; }
        public string? DeletedDate { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record PetCenterResponse
    {
        public Guid Id { get; init; }
        public string FullName { get; init; }
        public Gender? Gender { get; init; }
        public string? DateOfBirth { get; init; }
        public string? Avatar { get; init; }
        public string Email { get; init; }
        public string PhoneNumber { get; init; }


        public string CreatedDate { get; init; }
        public string? UpdatedDate { get; init; }
        public string? DeletedDate { get; init; }
        public bool IsDeleted { get; init; }
    }
}
