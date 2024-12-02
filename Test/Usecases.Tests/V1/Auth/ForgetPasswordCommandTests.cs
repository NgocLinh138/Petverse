using Application.Usecases.V1.Auth.Commands;
using Contract.Services.V1.Auth;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.Auth;

public sealed class ForgetPasswordCommandTests
{
    private readonly UserManager<AppUser> userManager;
    private readonly ForgetPasswordCommandHandler handler;
    private readonly Command.ForgetPasswordCommand request;
    public ForgetPasswordCommandTests()
    {
        this.userManager = A.Fake<UserManager<AppUser>>();
        this.handler = new ForgetPasswordCommandHandler(userManager);
        request = new Command.ForgetPasswordCommand
        {
            Email = "test@gmail.com",
            NewPassword = "newPassword"
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_When_EmailNotRegistered()
    {
        // Arrange

        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Không tìm thấy Email.");
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_WhenNewPassWordError()
    {
        // Arrange
        var user = new AppUser
        {
            Email = "test@gmail.com",
            UserName = "John"
        };
        string token = "token";
        A.CallTo(() => userManager.FindByEmailAsync(request.Email)).Returns(user);
        A.CallTo(() => userManager.GeneratePasswordResetTokenAsync(user)).Returns(token);
        A.CallTo(() => userManager.ResetPasswordAsync(user, token, request.NewPassword))
            .Returns(IdentityResult.Failed(new IdentityError
            {
                Code = "ResetPassword",
                Description = "IncorrectFomartPassword"
            }));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("IncorrectFomartPassword");
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess()
    {
        // Arrange
        var user = new AppUser
        {
            Email = "test@gmail.com",
            UserName = "John"
        };
        string token = "token";
        A.CallTo(() => userManager.FindByEmailAsync(request.Email)).Returns(user);
        A.CallTo(() => userManager.GeneratePasswordResetTokenAsync(user)).Returns(token);
        A.CallTo(() => userManager.ResetPasswordAsync(user, token, request.NewPassword))
            .Returns(IdentityResult.Success);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeNullOrEmpty();
        result.StatusCode.Should().Be(201);
    }
}
