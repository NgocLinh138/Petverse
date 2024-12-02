using Contract.Abstractions.Message;
using System.ComponentModel.DataAnnotations;

namespace Contract.Services.V1.Auth;
public static class Query
{
    public record LoginQuery : IQuery<Responses.UserAuthenticatedResponse>
    {
        [Required]
        public string Email { get; init; } = null!;
        [Required]
        public string Password { get; init; } = null!;
    }
    public record TokenQuery : IQuery<Responses.UserAuthenticatedResponse>
    {
        public string AccessToken { get; init; } = null!;
        public string? RefreshToken { get; init; }
        public DateTimeOffset RefreshTokenExpiryTime { get; init; }
    }
}
