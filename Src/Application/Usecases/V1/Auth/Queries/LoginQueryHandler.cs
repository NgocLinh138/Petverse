using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.JsonConverters;
using Contract.Services.V1;
using Contract.Services.V1.Auth;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.Auth.Queries;

public sealed class LoginQueryHandler : IQueryHandler<Query.LoginQuery, Responses.UserAuthenticatedResponse>
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly IJWTTokenService jWTTokenService;
    public LoginQueryHandler(
        UserManager<AppUser> userManager,
        IJWTTokenService jWTTokenService,
        RoleManager<AppRole> roleManager)
    {
        this.userManager = userManager;
        this.jWTTokenService = jWTTokenService;
        this.roleManager = roleManager;
    }

    public async Task<Result<Responses.UserAuthenticatedResponse>> Handle(Query.LoginQuery request, CancellationToken cancellationToken)
    {
        // Check User exist or active
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result.Failure<Responses.UserAuthenticatedResponse>("Email chưa được đăng ký.", StatusCodes.Status400BadRequest);
        if (user.IsDeleted == true)
            return Result.Failure<Responses.UserAuthenticatedResponse>("Người dùng đã bị khóa", StatusCodes.Status400BadRequest);
        // Check Password is correct
        if (!await userManager.CheckPasswordAsync(user, request.Password))
            return Result.Failure<Responses.UserAuthenticatedResponse>("Password không đúng.", StatusCodes.Status400BadRequest);

        var role = await roleManager.FindByIdAsync(user.RoleId.ToString());

        // Generate AccessToken
        var accessToken = await GenerateAccessToken(user, role.Name);

        // Generate RefreshToken
        var refreshToken = jWTTokenService.GenerateRefreshToken();

        // Response
        var response = new Responses.UserAuthenticatedResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTimeConverters.DateToString(TimeZones.GetSoutheastAsiaTime().AddYears(1)),
            UserId = user.Id,
            RoleName = role.Name
        };

        return Result.Success(response);
    }

    private async Task<string> GenerateAccessToken(AppUser user, string roleName)
    {
        var claims = await jWTTokenService.GenerateClaimsAsync(user.Id.ToString(), user.Email, roleName);

        var accessToken = jWTTokenService.GenerateAccessToken(claims);
        return accessToken;
    }
}
