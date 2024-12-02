using FluentValidation;

namespace Contract.Services.V1.Auth.Validators;

internal sealed class TokenQueryValidator : AbstractValidator<Query.TokenQuery>
{
    public TokenQueryValidator()
    {
    }
}
