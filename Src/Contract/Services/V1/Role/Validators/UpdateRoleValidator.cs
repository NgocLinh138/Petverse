using FluentValidation;

namespace Contract.Services.V1.Role.Validators;

internal sealed class UpdateRoleValidator : AbstractValidator<Command.UpdateRoleCommand>
{
    public UpdateRoleValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull();

        RuleFor(x => x.Description).NotEmpty().NotNull()
            .MaximumLength(200).WithMessage("Description least than 200 chars");
    }
}
