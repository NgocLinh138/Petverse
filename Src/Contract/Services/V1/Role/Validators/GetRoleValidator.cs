using FluentValidation;

namespace Contract.Services.V1.Role.Validators;

internal sealed class GetJobValidator : AbstractValidator<Query.GetRoleQuery>
{
    public GetJobValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThanOrEqualTo(0).WithMessage("PageIndex >= 0");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(5).WithMessage("PageSize >= 5");
    }
}
