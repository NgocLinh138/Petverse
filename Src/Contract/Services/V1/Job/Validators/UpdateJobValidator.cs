using FluentValidation;

namespace Contract.Services.V1.Job.Validators;

internal sealed class UpdateJobValidator : AbstractValidator<Command.UpdateJobCommand>
{
    public UpdateJobValidator()
    {
    }
}
