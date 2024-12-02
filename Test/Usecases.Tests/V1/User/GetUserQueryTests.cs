//using Contract.Abstractions.Shared;
//using Microsoft.EntityFrameworkCore;
//using Moq;

//namespace Usecases.Tests.V1.User;
//public class TestEntity
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}
//public sealed class GetUserQueryTests
//{
//    private List<TestEntity> GetTestEntities()
//    {
//        return new List<TestEntity>
//        {
//            new TestEntity { Id = 1, Name = "Item 1" },
//            new TestEntity { Id = 2, Name = "Item 2" },
//            new TestEntity { Id = 3, Name = "Item 3" },
//            new TestEntity { Id = 4, Name = "Item 4" },
//            new TestEntity { Id = 5, Name = "Item 5" }
//        };
//    }

//    [Fact]
//    public async Task CreateAsync_ShouldReturnPagedResult()
//    {
//        // Arrange
//        var data = GetTestEntities().AsQueryable();

//        var mockDbSet = new Mock<DbSet<TestEntity>>();
//        mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Provider).Returns(data.Provider);
//        mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.Expression).Returns(data.Expression);
//        mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
//        mockDbSet.As<IQueryable<TestEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

//        mockDbSet.Setup(x => x.CountAsync(CancellationToken.None))
//            .ReturnsAsync(data.Count());

//        mockDbSet.Setup(x => x.Skip(It.IsAny<int>()).Take(It.IsAny<int>()).ToListAsync(It.IsAny<CancellationToken>()))
//            .ReturnsAsync(data.Skip(0).Take(3).ToList());

//        // Act
//        var pagedResult = await PagedResult<TestEntity>.CreateAsync(mockDbSet.Object, pageIndex: 1, pageSize: 3);

//        // Assert
//        Assert.Equal(3, pagedResult.Items.Count); // Expecting 3 items on the first page
//        Assert.Equal(5, pagedResult.TotalCount); // Total count of the dataset
//        Assert.True(pagedResult.HasNextPage); // There should be a next page
//        Assert.False(pagedResult.HasPreviousPage); // This is the first page, so no previous page
//    }

//    //private readonly UserManager<AppUser> userManager;
//    //private readonly IMapper mapper;
//    //private readonly GetUserQueryHandler handler;
//    //private readonly Query.GetUserQuery request;

//    //public GetUserQueryTests()
//    //{
//    //    userManager = A.Fake<UserManager<AppUser>>();
//    //    mapper = A.Fake<IMapper>();
//    //    handler = new GetUserQueryHandler(userManager, mapper);

//    //}

//    //[Fact]
//    //public async Task Handle_Should_ReturnUsers_When_NoSearchTermProvided()
//    //{
//    //    // Arrange
//    //    var request = new Query.GetUserQuery
//    //    {
//    //        PageIndex = 1,
//    //        PageSize = 10,
//    //        SearchTerm = string.Empty,
//    //        RoleId = null
//    //    };
//    //    IQueryable<AppUser> users = new List<AppUser>
//    //    {
//    //        new AppUser { UserName = "User1", IsDeleted = false },
//    //        new AppUser { UserName = "User2", IsDeleted = false }
//    //    }.AsQueryable();

//    //    A.CallTo(() => userManager.Users).Returns(users);

//    //    A.CallTo(() => mapper.Map<PagedResult<Responses.UserGetAllResponse>>(A<PagedResult<AppUser>>.Ignored))
//    //        .Returns(new PagedResult<Responses.UserGetAllResponse>(new List<Responses.UserGetAllResponse>(), 2, 1, 10));

//    //    // Act
//    //    var result = await handler.Handle(request, CancellationToken.None);

//    //    // Assert
//    //    result.IsSuccess.Should().BeTrue();
//    //    result.Data.TotalCount.Should().Be(2);
//    //}

//    //[Fact]
//    //public async Task Handle_Should_FilterUsers_BySearchTerm()
//    //{
//    //    // Arrange
//    //    var request = new Query.GetUserQuery
//    //    {
//    //        PageIndex = 1,
//    //        PageSize = 10,
//    //        SearchTerm = "User1",
//    //        RoleId = null
//    //    };
//    //    var users = new List<AppUser>
//    //    {
//    //        new AppUser { UserName = "User1", IsDeleted = false },
//    //        new AppUser { UserName = "User2", IsDeleted = false }
//    //    }.AsQueryable();

//    //    A.CallTo(() => userManager.Users).Returns(users);
//    //    A.CallTo(() => mapper.Map<PagedResult<Responses.UserGetAllResponse>>(A<PagedResult<AppUser>>.Ignored))
//    //        .Returns(new PagedResult<Responses.UserGetAllResponse>(new List<Responses.UserGetAllResponse>(), 1, 1, 10));

//    //    // Act
//    //    var result = await handler.Handle(request, CancellationToken.None);

//    //    // Assert
//    //    result.IsSuccess.Should().BeTrue();
//    //    result.Data.TotalCount.Should().Be(1);
//    //}

//    //[Fact]
//    //public async Task Handle_Should_FilterUsers_ByRoleId()
//    //{
//    //    // Arrange
//    //    var request = new Query.GetUserQuery
//    //    {
//    //        PageIndex = 1,
//    //        PageSize = 10,
//    //        SearchTerm = string.Empty,
//    //        RoleId = Guid.NewGuid()
//    //    };
//    //    var users = new List<AppUser>
//    //    {
//    //        new AppUser { UserName = "User1", RoleId = request.RoleId.Value, IsDeleted = false },
//    //        new AppUser { UserName = "User2", RoleId = Guid.NewGuid(), IsDeleted = false }
//    //    }.AsQueryable();

//    //    A.CallTo(() => userManager.Users).Returns(users);
//    //    A.CallTo(() => mapper.Map<PagedResult<Responses.UserGetAllResponse>>(A<PagedResult<AppUser>>.Ignored))
//    //        .Returns(new PagedResult<Responses.UserGetAllResponse>(new List<Responses.UserGetAllResponse>(), 1, 1, 10));

//    //    // Act
//    //    var result = await handler.Handle(request, CancellationToken.None);

//    //    // Assert
//    //    result.IsSuccess.Should().BeTrue();
//    //    result.Data.TotalCount.Should().Be(1);
//    //}

//    //[Fact]
//    //public async Task Handle_Should_ReturnPagedResults_When_PageIndexAndPageSizeProvided()
//    //{
//    //    // Arrange
//    //    var request = new Query.GetUserQuery
//    //    {
//    //        PageIndex = 2,
//    //        PageSize = 1,
//    //        SearchTerm = string.Empty,
//    //        RoleId = null
//    //    };
//    //    var users = new List<AppUser>
//    //    {
//    //        new AppUser { UserName = "User1", IsDeleted = false },
//    //        new AppUser { UserName = "User2", IsDeleted = false }
//    //    }.AsQueryable();

//    //    A.CallTo(() => userManager.Users).Returns(users);
//    //    A.CallTo(() => mapper.Map<PagedResult<Responses.UserGetAllResponse>>(A<PagedResult<AppUser>>.Ignored))
//    //        .Returns(new PagedResult<Responses.UserGetAllResponse>(new List<Responses.UserGetAllResponse>(), 2, 2, 1));

//    //    // Act
//    //    var result = await handler.Handle(request, CancellationToken.None);

//    //    // Assert
//    //    result.IsSuccess.Should().BeTrue();
//    //    result.Data.PageIndex.Should().Be(2);
//    //    result.Data.PageSize.Should().Be(1);
//    //}
//}
