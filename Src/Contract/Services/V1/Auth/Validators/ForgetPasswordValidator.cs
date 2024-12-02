using FluentValidation;

namespace Contract.Services.V1.Auth.Validators;

internal sealed class ForgetPasswordValidator : AbstractValidator<Command.ForgetPasswordCommand>
{
    public ForgetPasswordValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email not InValid");

        RuleFor(x => x.NewPassword)
            .MinimumLength(6).WithMessage("Password Have MinLength is 6");
    }
}
