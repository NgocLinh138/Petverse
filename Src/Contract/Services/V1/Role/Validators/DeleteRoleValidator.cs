using FluentValidation;

namespace Contract.Services.V1.Role.Validators;

internal sealed class DeleteRoleValidator : AbstractValidator<Command.DeleteRoleCommand>
{
    public DeleteRoleValidator()
    {
    }
}
