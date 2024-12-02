using FluentValidation;

namespace Contract.Services.V1.Auth.Validators;

internal sealed class LoginQueryValidator : AbstractValidator<Query.LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotNull();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull();
    }
}
