using Contract.Abstractions.Message;

namespace Contract.Services.V1.Auth;
public static class Command
{
    public record ForgetPasswordCommand : ICommand
    {
        public string Email { get; init; } = null!;
        public string NewPassword { get; init; } = null!;
    }
}