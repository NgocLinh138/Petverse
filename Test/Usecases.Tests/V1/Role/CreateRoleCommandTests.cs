using Application.Usecases.V1.Role.Commands;
using AutoMapper;
using Contract.Services.V1.Role;
using Domain.Abstractions;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.Role;

public sealed class CreateRoleCommandTests
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly CreateRoleCommandHandler handler;
    private readonly Command.CreateRoleCommand request;

    public CreateRoleCommandTests()
    {
        this.roleManager = A.Fake<RoleManager<AppRole>>();
        this.mapper = A.Fake<IMapper>();
        this.unitOfWork = A.Fake<IUnitOfWork>();
        this.handler = new CreateRoleCommandHandler(roleManager, mapper, unitOfWork);
        this.request = new Command.CreateRoleCommand("Admin", "Administrator role");
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_When_RoleAlreadyExists()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByNameAsync(request.Name))
            .Returns(Task.FromResult<AppRole?>(new AppRole { Name = "Admin" }));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_RoleCreatedSuccessfully()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByNameAsync(request.Name))
            .Returns(Task.FromResult<AppRole?>(null)); // Role không tồn tại
        A.CallTo(() => roleManager.CreateAsync(A<AppRole>.Ignored))
            .Returns(Task.FromResult(IdentityResult.Success));

        var roleResponse = new Responses.RoleResponse
        {
            Name = "Admin",
            Description = "Administrator role"
        };
        A.CallTo(() => mapper.Map<Responses.RoleResponse>(A<AppRole>.Ignored)).Returns(roleResponse);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeNullOrEmpty();
        result.StatusCode.Should().Be(201);
        result.Data.Should().BeEquivalentTo(roleResponse);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_CreateRoleFails()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByNameAsync(request.Name))
            .Returns(Task.FromResult<AppRole?>(null)); // Role không tồn tại
        A.CallTo(() => roleManager.CreateAsync(A<AppRole>.Ignored))
            .Throws(new Exception("Error creating role"));

        // Act
        Func<Task> action = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<Exception>()
            .WithMessage("Error creating role");
    }
}
