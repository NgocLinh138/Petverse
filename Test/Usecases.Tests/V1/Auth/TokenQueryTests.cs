using Application.Usecases.V1.Auth.Queries;
using Contract.Constants;
using Contract.Services.V1;
using Contract.Services.V1.Auth;
using FakeItEasy;
using FluentAssertions;
using System.Security.Claims;

namespace Usecases.Tests.V1.Auth;

public sealed class TokenQueryTests
{
    private readonly IJWTTokenService jWTTokenService;
    private readonly TokenQueryHandler handler;
    public TokenQueryTests()
    {
        this.jWTTokenService = A.Fake<IJWTTokenService>();
        handler = new TokenQueryHandler(jWTTokenService);
    }

    [Fact]
    public async Task Handle_Should_ReturnFail_WhenRefreshTokenExpiredTime()
    {
        // Arrange
        var request = new Query.TokenQuery
        {
            RefreshTokenExpiryTime = DateTime.Now.AddDays(-1),
        };

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        //result.Message.Should().Be("Token Expired");
        result.StatusCode.Should().Be(403);
    }

    [Fact]
    public async Task Handle_Should_ThrowException_WhenTokenIsInvalid()
    {
        // Arrange
        var request = new Query.TokenQuery
        {
            AccessToken = "someInvalidAccessToken",
            RefreshTokenExpiryTime = TimeZones.GetSoutheastAsiaTime().AddYears(1)
        };

        A.CallTo(() => jWTTokenService.GetPrincipalFromExpiredToken(request.AccessToken))
            .Throws(new Exception("Invalid token"));

        // Act
        Func<Task> action = async () => await handler.Handle(request, CancellationToken.None);

        // & Assert
        await action.Should().ThrowAsync<Exception>().WithMessage("Invalid token");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessful()
    {
        // Arrange
        var principle = A.Fake<ClaimsPrincipal>();
        var request = new Query.TokenQuery
        {
            AccessToken = "accessToken",
            RefreshTokenExpiryTime = DateTime.Now.AddDays(1),
            RefreshToken = "refreshToken"
        };

        A.CallTo(() => jWTTokenService.GetPrincipalFromExpiredToken(request.AccessToken)).Returns(principle);
        A.CallTo(() => jWTTokenService.GenerateAccessToken(principle.Claims)).Returns("newAccessToken");
        A.CallTo(() => jWTTokenService.GenerateRefreshToken()).Returns("newRefreshToken");

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Message.Should().BeEmpty();
        result.StatusCode.Should().Be(200);
        result.Data.AccessToken.Should().Be("newAccessToken");
        result.Data.RefreshToken.Should().Be("newRefreshToken");
    }
}

