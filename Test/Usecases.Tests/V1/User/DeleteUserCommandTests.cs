//using Application.Usecases.V1.User.Commands;
//using Contract.Services.V1.User;
//using Domain.Entities.Identity;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;

//namespace Usecases.Tests.V1.User;

//public sealed class DeleteUserCommandTests
//{
//    private readonly UserManager<AppUser> userManager;
//    private readonly DeleteUserCommandHandler handler;
//    private readonly Command.DeleteUserCommand request;
//    private readonly RoleManager<AppRole> roleManager;
//    public DeleteUserCommandTests()
//    {
//        userManager = A.Fake<UserManager<AppUser>>();
//        roleManager = A.Fake<RoleManager<AppRole>>();
//        handler = new DeleteUserCommandHandler(userManager, roleManager);
//        request = new Command.DeleteUserCommand(Guid.NewGuid());
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_UserNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.Id.ToString()))
//            .Returns(Task.FromResult<AppUser?>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFailure_When_UserIsAdmin()
//    {
//        // Arrange
//        var user = new AppUser { Id = request.Id, RoleId = Guid.NewGuid() };
//        A.CallTo(() => userManager.FindByIdAsync(request.Id.ToString()))
//            .Returns(Task.FromResult(user));

//        A.CallTo(() => roleManager.FindByIdAsync(user.RoleId.ToString()))
//            .Returns(Task.FromResult<AppRole>(new AppRole { Name = "admin" }));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_When_UserDeletedSuccessfully()
//    {
//        // Arrange
//        var user = new AppUser { Id = request.Id };
//        A.CallTo(() => userManager.FindByIdAsync(request.Id.ToString()))
//            .Returns(Task.FromResult(user));

//        A.CallTo(() => userManager.GetRolesAsync(user))
//            .Returns(Task.FromResult<IList<string>>(new List<string>()));

//        A.CallTo(() => userManager.UpdateAsync(user))
//            .Returns(Task.FromResult(IdentityResult.Success));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.StatusCode.Should().Be(202);
//    }
//}
