//using Application.Usecases.V1.User.Commands;
//using AutoMapper;
//using Contract.Services.V1.User;
//using Domain.Entities.Identity;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Identity;

//namespace Usecases.Tests.V1.User;

//public sealed class AssignUserCommandTests
//{
//    private readonly RoleManager<AppRole> roleManager;
//    private readonly UserManager<AppUser> userManager;
//    private readonly IMapper mapper;
//    private readonly AssignRoleCommandHandler handler;
//    private readonly Command.AssignRoleCommand request;

//    public AssignUserCommandTests()
//    {
//        this.roleManager = A.Fake<RoleManager<AppRole>>();
//        this.userManager = A.Fake<UserManager<AppUser>>();
//        this.mapper = A.Fake<IMapper>();
//        this.handler = new AssignRoleCommandHandler(roleManager, userManager, mapper);
//        this.request = new Command.AssignRoleCommand
//        {
//            UserId = Guid.NewGuid(),
//            RoleId = Guid.NewGuid()
//        };
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFail_When_UserNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString()))
//            .Returns(Task.FromResult<AppUser?>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(404);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFail_When_RoleNotFound()
//    {
//        // Arrange
//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString()))
//            .Returns(Task.FromResult(new AppUser { Id = request.UserId }));

//        A.CallTo(() => roleManager.FindByIdAsync(request.RoleId.ToString()))
//            .Returns(Task.FromResult<AppRole?>(null));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeFalse();
//        result.StatusCode.Should().Be(404);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnSuccess_When_RoleAssignedSuccessfully()
//    {
//        // Arrange
//        var appUser = new AppUser { Id = request.UserId };
//        var appRole = new AppRole { Id = request.RoleId };

//        A.CallTo(() => userManager.FindByIdAsync(request.UserId.ToString()))
//            .Returns(Task.FromResult(appUser));

//        A.CallTo(() => roleManager.FindByIdAsync(request.RoleId.ToString()))
//            .Returns(Task.FromResult(appRole));

//        A.CallTo(() => userManager.UpdateAsync(appUser))
//            .Returns(Task.FromResult(IdentityResult.Success));

//        var userResponse = new Responses.UserResponse
//        {
//            Id = request.UserId,
//            RoleId = request.RoleId
//        };

//        A.CallTo(() => mapper.Map<Responses.UserResponse>(appUser))
//            .Returns(userResponse);

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Data.Should().BeEquivalentTo(userResponse);
//    }
//}
