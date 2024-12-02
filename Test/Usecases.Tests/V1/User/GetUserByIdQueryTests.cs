//using Application.Usecases.V1.User.Queries;
//using Contract.Enumerations;
//using Contract.JsonConverters;
//using Contract.Services.V1.User;
//using Domain.Entities.Identity;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;

//namespace Usecases.Tests.V1.User;
//public sealed class GetUserByIdQueryTests
//{
//    private readonly UserManager<AppUser> userManager;
//    private readonly GetUserIdQueryHandler handler;
//    private readonly Query.GetUserByIdQuery request;

//    public GetUserByIdQueryTests()
//    {
//        userManager = A.Fake<UserManager<AppUser>>();
//        handler = new GetUserIdQueryHandler(userManager);
//        request = new Query.GetUserByIdQuery(Guid.NewGuid());
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
//    public async Task Handle_Should_ReturnSuccess_When_UserFound()
//    {
//        // Arrange
//        var user = new AppUser
//        {
//            Id = request.Id,
//            RoleId = Guid.NewGuid(),
//            Avatar = "avatar.png",
//            Gender = Gender.Male,
//            DateOfBirth = new DateTime(1990, 1, 1),
//            Email = "test@example.com",
//            PhoneNumber = "123456789",
//            FullName = "John Doe",
//            CreatedDate = DateTime.UtcNow.AddYears(-1),
//            UpdatedDate = DateTime.UtcNow,
//            DeletedDate = null,
//            IsDeleted = false
//        };

//        A.CallTo(() => userManager.FindByIdAsync(request.Id.ToString()))
//            .Returns(Task.FromResult(user));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Data.Should().BeEquivalentTo(new Responses.UserGetByIdResponse
//        {
//            Id = user.Id,
//            RoleId = user.RoleId,
//            Avatar = user.Avatar,
//            Gender = user.Gender,
//            DateOfBirth = DateTimeConverters.DateToString(user.DateOfBirth),
//            Email = user.Email,
//            PhoneNumber = user.PhoneNumber,
//            FullName = user.FullName,
//            CreatedDate = DateTimeConverters.DateToString(user.CreatedDate),
//            UpdatedDate = DateTimeConverters.DateToString(user.UpdatedDate),
//            DeletedDate = DateTimeConverters.DateToString(user.DeletedDate),
//            IsDeleted = user.IsDeleted
//        });
//    }
//}
