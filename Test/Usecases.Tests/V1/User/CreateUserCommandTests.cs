using Application.Usecases.V1.User.Commands;
using AutoMapper;
using Contract.Services.V1.User;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.User;

public sealed class CreateUserCommandTests
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;
    private readonly CreateUserCommandHandler handler;
    private readonly Command.CreateUserCommand request;

    public CreateUserCommandTests()
    {
        this.userManager = A.Fake<UserManager<AppUser>>();
        this.roleManager = A.Fake<RoleManager<AppRole>>();
        this.mapper = A.Fake<IMapper>();
        this.handler = new CreateUserCommandHandler(userManager, roleManager, mapper);
        this.request = new Command.CreateUserCommand
        (
            FullName: "John Doe",
            Email: "john.doe@example.com",
            Password: "Password123!",
            PhoneNumber: "123456789"
        );
    }


    [Fact]
    public async Task Handle_Should_ReturnFail_When_EmailAlreadyRegistered()
    {
        // Arrange
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult(new AppUser { Email = "john.doe@example.com" }));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_When_RoleNotFound()
    {
        // Arrange
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(null));

        var role = new AppRole { Id = Guid.NewGuid(), Name = "Customer" };
        A.CallTo(() => roleManager.FindByNameAsync("customer"))
            .Returns(Task.FromResult<AppRole?>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_UserCreatedSuccessfully()
    {
        // Arrange
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(null));

        var role = new AppRole { Id = Guid.NewGuid(), Name = "Customer" };
        A.CallTo(() => roleManager.FindByNameAsync("customer"))
            .Returns(Task.FromResult(role));

        A.CallTo(() => userManager.CreateAsync(A<AppUser>.Ignored, request.Password))
            .Returns(Task.FromResult(IdentityResult.Success));

        var userResponse = new Responses.UserResponse
        {
            Id = Guid.NewGuid(),
            RoleId = role.Id,
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber
        };

        A.CallTo(() => mapper.Map<Responses.UserResponse>(A<AppUser>.Ignored)).Returns(userResponse);
        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeNullOrEmpty();
        result.Data.Should().BeEquivalentTo(userResponse);
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_When_UserCreationFails()
    {
        // Arrange
        A.CallTo(() => userManager.FindByEmailAsync(request.Email))
            .Returns(Task.FromResult<AppUser?>(null)); // User does not exist

        var role = new AppRole { Id = Guid.NewGuid(), Name = "Customer" };
        A.CallTo(() => roleManager.FindByNameAsync("customer"))
            .Returns(Task.FromResult(role));

        var identityErrors = new IdentityError[] { new IdentityError { Description = "Password Too Weak" } };
        A.CallTo(() => userManager.CreateAsync(A<AppUser>.Ignored, request.Password))
            .Returns(Task.FromResult(IdentityResult.Failed(identityErrors)));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }
}
