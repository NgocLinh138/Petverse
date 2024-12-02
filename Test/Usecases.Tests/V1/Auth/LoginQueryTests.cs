using Application.Usecases.V1.Auth.Queries;
using Contract.Services.V1;
using Contract.Services.V1.Auth;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Usecases.Tests.V1.Auth;

public sealed class LoginQueryTests
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly IJWTTokenService jWTTokenService;
    private readonly LoginQueryHandler handler;

    public LoginQueryTests()
    {
        userManager = A.Fake<UserManager<AppUser>>();
        roleManager = A.Fake<RoleManager<AppRole>>();
        jWTTokenService = A.Fake<IJWTTokenService>();
        handler = new LoginQueryHandler(userManager, jWTTokenService, roleManager);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new Query.LoginQuery
        {
            Email = "test@example.com",
            Password = "Password123"
        };
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        //result.Message.Should().Be("Email is not register");
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenPasswordIsIncorrect()
    {
        // Arrange
        var request = new Query.LoginQuery { Email = "test@example.com", Password = "Password123" };
        var user = new AppUser { Email = "test@example.com" };
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(user));
        A.CallTo(() => userManager.CheckPasswordAsync(user, request.Password)).Returns(Task.FromResult(false));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        //result.Message.Should().Be("Password incorrect");
    }

    [Fact]
    public async Task Handle_Should_Return_Success_WhenCredentialsAreValid()
    {
        // Arrange
        var request = new Query.LoginQuery { Email = "test@example.com", Password = "Password123" };
        var user = new AppUser { Id = Guid.NewGuid(), Email = "test@example.com", FullName = "John Doe" };
        var role = new AppRole { Id = Guid.NewGuid(), Name = "Admin" };
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "Admin")
        };

        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(user));
        A.CallTo(() => userManager.CheckPasswordAsync(user, request.Password))
            .Returns(Task.FromResult(true));
        A.CallTo(() => roleManager.FindByIdAsync(user.RoleId.ToString()))
            .Returns(Task.FromResult<AppRole?>(role));
        A.CallTo(() => jWTTokenService.GenerateClaimsAsync(user.Id.ToString(), user.Email, role.Name))
            .Returns(claims);
        A.CallTo(() => jWTTokenService.GenerateAccessToken(claims)).Returns("accessToken");
        A.CallTo(() => jWTTokenService.GenerateRefreshToken()).Returns("refreshToken");

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.RefreshToken.Should().Be("refreshToken");
        result.Data.AccessToken.Should().Be("accessToken");
    }
}
