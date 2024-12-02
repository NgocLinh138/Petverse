using Application.Usecases.V1.Role.Queries;
using AutoMapper;
using Contract.Services.V1.Role;
using Domain.Entities.Identity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Usecases.Tests.V1.Role;

public sealed class GetRoleByIdQueryTests
{
    private readonly RoleManager<AppRole> roleManager;
    private readonly IMapper mapper;
    private readonly GetRoleByIdQueryHandler handler;
    private readonly Query.GetRoleByIdQuery request;
    private readonly AppRole role;

    public GetRoleByIdQueryTests()
    {
        roleManager = A.Fake<RoleManager<AppRole>>();
        mapper = A.Fake<IMapper>();
        handler = new GetRoleByIdQueryHandler(roleManager, mapper);

        request = new Query.GetRoleByIdQuery
        {
            Id = Guid.NewGuid(),
        };
        role = new AppRole
        {
            Id = request.Id,
            Name = "Admin",
            Description = "Administrator role"
        };
    }

    [Fact]
    public async Task Handle_Should_ReturnRole_When_RoleExists()
    {
        // Arrange
        A.CallTo(() => roleManager.FindByIdAsync(request.Id.ToString()))
            .Returns(Task.FromResult<AppRole?>(role)); // Vai trò tồn tại
        A.CallTo(() => mapper.Map<Responses.RoleResponse>(role))
            .Returns(new Responses.RoleResponse { Id = role.Id, Name = role.Name, Description = role.Description }); // Giả lập mapper

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Name.Should().Be(role.Name);
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
        //result.Message.Should().Be("Role Not Found");
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
