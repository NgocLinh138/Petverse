using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Application.Usecases.V1.User.Commands;
public sealed class CreateUserCommandHandler : ICommandHandler<Command.CreateUserCommand, Responses.UserResponse>
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager,
        IMapper mapper)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.CreateUserCommand request, CancellationToken cancellationToken)
    {

        var userExist = await userManager.FindByEmailAsync(request.Email);
        if (userExist != null)
            return Result.Failure<Responses.UserResponse>("Email đã được đăng ký.", StatusCodes.Status400BadRequest);

        // Get RoleCustomer
        var roleCustomer = await roleManager.FindByNameAsync("customer");
        if (roleCustomer == null)
            return Result.Failure<Responses.UserResponse>("Không tìm thấy role người dùng.", StatusCodes.Status404NotFound);

        // CreateUser
        var user = new AppUser
        {
            UserName = request.Email,
            RoleId = roleCustomer.Id,
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        var createUserResult = await userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var error in createUserResult.Errors)
                stringBuilder.Append(error.Description + ",");

            return Result.Failure<Responses.UserResponse>(stringBuilder.ToString(), StatusCodes.Status400BadRequest);
        }

        var response = mapper.Map<Responses.UserResponse>(user);
        return Result.Success(response, 201);
    }
}
