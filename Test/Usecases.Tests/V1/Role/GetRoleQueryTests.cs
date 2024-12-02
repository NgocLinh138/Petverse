//using Application.Usecases.V1.Role.Queries;
//using AutoMapper;
//using Contract.Abstractions.Shared;
//using Contract.Services.V1.Role;
//using Domain.Entities.Identity;
//using FakeItEasy;
//using FluentAssertions;
//using Microsoft.AspNetCore.Identity;

//namespace Usecases.Tests.V1.Role;

//public sealed class GetRoleQueryTests
//{
//    private readonly RoleManager<AppRole> roleManager;
//    private readonly IMapper mapper;
//    private readonly GetRoleQueryHandler handler;
//    private IQueryable<AppRole> roles;
//    public GetRoleQueryTests()
//    {
//        this.roleManager = A.Fake<RoleManager<AppRole>>();
//        this.mapper = A.Fake<IMapper>();
//        this.handler = new GetRoleQueryHandler(roleManager, mapper);
//        roles = new List<AppRole>
//            {
//                new AppRole { Name = "Admin" },
//                new AppRole { Name = "User" },
//                new AppRole { Name = "Moderator" }
//            }.AsQueryable();
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnRoles_When_SearchTermIsNullOrWhiteSpace()
//    {
//        // Arrange

//        A.CallTo(() => roleManager.Roles).Returns(roles);

//        var request = new Query.GetRoleQuery
//        {
//            SearchTerm = null, // Hoặc có thể để trống
//            PageIndex = 1,
//            PageSize = 10
//        };

//        var roleResponse = new Responses.RoleResponse
//        {
//            Name = "Admin"
//        };

//        A.CallTo(() => mapper.Map<PagedResult<Responses.RoleResponse>>(A<PagedResult<AppRole>>.Ignored))
//            .Returns(new PagedResult<Responses.RoleResponse>(new List<Responses.RoleResponse> { roleResponse }, 1, 1, 1));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Data.Should().NotBeNull();
//        result.Data.Items.Should().HaveCount(1);
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnFilteredRoles_When_SearchTermIsProvided()
//    {
//        // Arrange
//        A.CallTo(() => roleManager.Roles).Returns(roles);

//        var request = new Query.GetRoleQuery
//        {
//            SearchTerm = "Admin",
//            PageIndex = 1,
//            PageSize = 10
//        };

//        var roleResponse = new Responses.RoleResponse
//        {
//            Name = "Admin"
//        };

//        A.CallTo(() => mapper.Map<PagedResult<Responses.RoleResponse>>(A<PagedResult<AppRole>>.Ignored))
//            .Returns(new PagedResult<Responses.RoleResponse>(new List<Responses.RoleResponse> { roleResponse }, 1, 1, 1));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Data.Should().NotBeNull();
//        result.Data.Items.Should().HaveCount(1);
//        result.Data.Items.First().Name.Should().Be("Admin");
//    }

//    [Fact]
//    public async Task Handle_Should_ReturnEmpty_When_NoRolesMatchSearchTerm()
//    {
//        // Arrange
//        A.CallTo(() => roleManager.Roles).Returns(roles);

//        var request = new Query.GetRoleQuery
//        {
//            SearchTerm = "Admin",
//            PageIndex = 1,
//            PageSize = 10
//        };

//        A.CallTo(() => mapper.Map<PagedResult<Responses.RoleResponse>>(A<PagedResult<AppRole>>.Ignored))
//            .Returns(new PagedResult<Responses.RoleResponse>(new List<Responses.RoleResponse>(), 0, 1, 1));

//        // Act
//        var result = await handler.Handle(request, CancellationToken.None);

//        // Assert
//        result.IsSuccess.Should().BeTrue();
//        result.Data.Should().NotBeNull();
//        result.Data.Items.Should().BeEmpty();
//    }

//    [Fact]
//    public async Task Handle_Should_ThrowException_When_ErrorOccurs()
//    {
//        // Arrange
//        A.CallTo(() => roleManager.Roles).Throws(new Exception("Database error"));

//        var request = new Query.GetRoleQuery
//        {
//            SearchTerm = null,
//            PageIndex = 1,
//            PageSize = 10
//        };

//        // Act
//        Func<Task> action = async () => await handler.Handle(request, CancellationToken.None);

//        // Assert
//        await action.Should().ThrowAsync<Exception>().WithMessage("Database error");
//    }
//}


