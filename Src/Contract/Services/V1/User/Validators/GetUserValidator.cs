using FluentValidation;

namespace Contract.Services.V1.User.Validators;

internal sealed class GetUserValidator : AbstractValidator<Query.GetUserQuery>
{
    public GetUserValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(1)
            .WithMessage("PageIndex >= 1.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 10)
            .WithMessage("PageSize between 1 and 10.");
    }
}
