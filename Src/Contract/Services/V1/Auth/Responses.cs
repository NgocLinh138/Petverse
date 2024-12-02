namespace Contract.Services.V1.Auth;
public static class Responses
{
    public class UserAuthenticatedResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string RefreshTokenExpiryTime { get; set; }
        public Guid UserId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
