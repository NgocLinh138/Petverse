using Application.Usecases.V1.Role.Commands;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.Role;

public sealed class DeleteRoleCommandTests
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly DeleteRoleCommandHandler handler;
    private readonly Command.DeleteRoleCommand request;
    private readonly AppRole role;

    public DeleteRoleCommandTests()
    {
        // Thiết lập các giả lập cho RoleManager
        roleManager = A.Fake<RoleManager<AppRole>>();
        handler = new DeleteRoleCommandHandler(roleManager);

        // Khởi tạo request và role
        request = new Command.DeleteRoleCommand(Guid.NewGuid());

        role = new AppRole
        {
            Id = Guid.NewGuid(),
            Name = "Admin",
            Description = "Administrator role"
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_DeleteRoleIsSuccessful()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(role)); // Vai trò tồn tại
        A.CallTo(() => roleManager.DeleteAsync(role))
            .Returns(Task.FromResult(IdentityResult.Success)); // Xóa thành công

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
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
        result.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_When_DeleteRoleFails()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(role)); // Vai trò tồn tại
        A.CallTo(() => roleManager.DeleteAsync(role))
            .Throws(new Exception("Error deleting role")); // Xóa thất bại

        // Act
        Func<Task> action = async () => await handler.Handle(request, CancellationToken.None);

        // Assert
        await action.Should().ThrowAsync<Exception>()
            .WithMessage("Error deleting role");
    }
}
