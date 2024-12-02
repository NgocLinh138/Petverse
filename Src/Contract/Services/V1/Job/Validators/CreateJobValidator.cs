using FluentValidation;

namespace Contract.Services.V1.Job.Validators;

public sealed class CreateJobValidator : AbstractValidator<Command.CreateJobCommand>
{
    public CreateJobValidator()
    {
        RuleFor(x => x.PetCenterId)
            .NotNull().WithMessage("PetSitterId not null");

    }
}
