using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.JsonConverters;
using Contract.Services.V1;
using Contract.Services.V1.Auth;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Auth.Queries;

public sealed class TokenQueryHandler : IQueryHandler<Query.TokenQuery, Responses.UserAuthenticatedResponse>
{
    private readonly IJWTTokenService jwtTokenService;

    public TokenQueryHandler(IJWTTokenService jwtTokenService)
    {
        this.jwtTokenService = jwtTokenService;
    }

    public async Task<Result<Responses.UserAuthenticatedResponse>> Handle(Query.TokenQuery request, CancellationToken cancellationToken)
    {
        if (request.RefreshTokenExpiryTime < TimeZones.GetSoutheastAsiaTime())
            return Result.Failure<Responses.UserAuthenticatedResponse>("Token đã hết hạn.", StatusCodes.Status403Forbidden);

        try
        {
            var principle = jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

            var newAccessToken = jwtTokenService.GenerateAccessToken(principle.Claims);
            var newRefreshToken = jwtTokenService.GenerateRefreshToken();

            var response = new Responses.UserAuthenticatedResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = DateTimeConverters.DateToString(TimeZones.GetSoutheastAsiaTime().AddYears(1)),
            };
            return Result.Success(response);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
