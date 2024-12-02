using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.JsonConverters;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.User.Queries;
public sealed class GetUserIdQueryHandler : IQueryHandler<Query.GetUserByIdQuery, Responses.UserGetByIdResponse>
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    public GetUserIdQueryHandler(
        UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public async Task<Result<Responses.UserGetByIdResponse>> Handle(Query.GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Check UserExist
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            return Result.Failure<Responses.UserGetByIdResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);
        var role = await roleManager.FindByIdAsync(user.RoleId.ToString());

        var response = new Responses.UserGetByIdResponse
        {
            Id = user.Id,
            PetCenterId = user.Application?.PetCenter?.Id,
            RoleId = user.RoleId,
            RoleName = role.Name,
            Avatar = user.Avatar,
            Gender = user.Gender,
            DateOfBirth = DateTimeConverters.DateToString(user.DateOfBirth),
            Address = user.Address,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FullName = user.FullName,
            CreatedDate = DateTimeConverters.DateToString(user.CreatedDate),
            UpdatedDate = DateTimeConverters.DateToString(user.UpdatedDate),
            DeletedDate = DateTimeConverters.DateToString(user.DeletedDate),
            IsDeleted = user.IsDeleted,
            Balance = user.Balance,
        };

        return Result.Success(response);
    }

}
