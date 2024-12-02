using Contract.Abstractions.Message;
using Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.User;
public class Command
{
    public record CreateUserCommand(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber) : ICommand<Responses.UserResponse>;

    public record UpdateUserCommand(
        Guid? Id,
        string? FullName,
        Gender? Gender,
        string? DateOfBirth,
        IFormFile? Avatar,
        string? PhoneNumber,
        string? Address) : ICommand<Responses.UserGetByIdResponse>;

    public record AssignRoleCommand(
        Guid UserId,
        Guid RoleId) : ICommand<Responses.UserResponse>;
    public record DeleteUserCommand(Guid Id) : ICommand;
}
