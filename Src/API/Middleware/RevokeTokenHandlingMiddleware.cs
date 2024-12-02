using Contract.Services.V1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;

namespace API.Middleware;

public class RevokeTokenHandlingMiddleware : IMiddleware
{
    private readonly IRedisCacheService redisCacheService;
    public RevokeTokenHandlingMiddleware(IRedisCacheService redisCacheService)
    {
        this.redisCacheService = redisCacheService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
        {
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken != null)
                {
                    // Lấy UserId từ JWT Token
                    var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(userIdClaim))
                    {
                        var isRevoked = await redisCacheService.IsUserRevokedAsync(userIdClaim);
                        if (isRevoked)
                        {
                            await HandleResponseAsync(context, StatusCodes.Status403Forbidden, "Người dùng đã bị khóa");
                            await redisCacheService.RemoveCacheResponseAsync(userIdClaim);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await HandleResponseAsync(context, StatusCodes.Status401Unauthorized, ex.Message);
                return;
            }
        }

        await next(context);

    }

    private async Task HandleResponseAsync(HttpContext httpContext, int statusCode, string message)
    {
        var response = new
        {
            isSuccess = false,
            statusCode,
            message
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
