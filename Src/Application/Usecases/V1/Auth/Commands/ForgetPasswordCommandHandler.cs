using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Auth;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Application.Usecases.V1.Auth.Commands;

public sealed class ForgetPasswordCommandHandler : ICommandHandler<Command.ForgetPasswordCommand>
{
    private readonly UserManager<AppUser> userManager;

    public ForgetPasswordCommandHandler(UserManager<AppUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task<Result> Handle(Command.ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        // Check user is exist
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return Result.Failure("Không tìm thấy Email.", StatusCodes.Status404NotFound);

        // Generate Password reset Token
        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        // Reset Password
        var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            StringBuilder errors = new StringBuilder();
            foreach (var error in result.Errors)
            {
                errors.Append(error.Description);
            }

            return Result.Failure(errors.ToString());
        }

        return Result.Success(201);

    }
}
