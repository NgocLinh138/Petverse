using FluentValidation;

namespace Contract.Services.V1.Role.Validators;

internal sealed class CreateJobValidator : AbstractValidator<Command.CreateRoleCommand>
{
    public CreateJobValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();
    }
}
