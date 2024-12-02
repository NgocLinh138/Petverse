using Application.Usecases.V1.Role.Commands;
using AutoMapper;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.Role;

public sealed class UpdateRoleCommandTests
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;
    private readonly UpdateRoleCommandHandler handler;
    private Command.UpdateRoleCommand request;
    private AppRole role;

    public UpdateRoleCommandTests()
    {
        // Thiết lập các giả lập cho RoleManager và IMapper
        roleManager = A.Fake<RoleManager<AppRole>>();
        mapper = A.Fake<IMapper>();
        handler = new UpdateRoleCommandHandler(roleManager, mapper);
        request = new Command.UpdateRoleCommand
        (
            Id: Guid.NewGuid(),
            Name: "Admin",
            Description: "Administrator role"
        );

        role = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = "OldName",
            Description = "OldDescription"
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_UpdateRoleIsSuccessful()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(role)); // Vai trò tồn tại
        A.CallTo(() => roleManager.UpdateAsync(role))
            .Returns(Task.FromResult(IdentityResult.Success)); // Cập nhật thành công

        var expectedResponse = new Responses.RoleResponse
        {
            Id = role.Id,
            Name = request.Name,
            Description = request.Description
        };

        A.CallTo(() => mapper.Map<Responses.RoleResponse>(role))
            .Returns(expectedResponse);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_RoleNotFound()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(null)); // Vai trò không tồn tại

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_UpdateRoleFails()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(role)); // Vai trò tồn tại
        A.CallTo(() => roleManager.UpdateAsync(role))
            .Throws(new Exception("Error updating role")); // Cập nhật thất bại

        // Act
        Func<Task> action = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<Exception>()
            .WithMessage("Error updating role");
    }
}
