using FluentValidation;

namespace Contract.Services.V1.Job.Validators;

internal sealed class GetJobValidator : AbstractValidator<Query.GetJobQuery>
{
    public GetJobValidator()
    {
        RuleFor(x => x.PetServiceId)
            .GreaterThan(0)
            .When(x => x.PetServiceId.HasValue)
            .WithMessage("PetServiceId > 0.");

        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("PageIndex > 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize > 0.");
    }
}
