using System.Security.Claims;

namespace Contract.Services.V1;
public interface IJWTTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    Task<IEnumerable<Claim>> GenerateClaimsAsync(string userId, string email, string role);
}
