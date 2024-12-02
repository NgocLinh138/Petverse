using FluentValidation;

namespace Contract.Services.V1.Role.Validators;

internal sealed class GetRoleByIdValidator : AbstractValidator<Query.GetRoleByIdQuery>
{
    public GetRoleByIdValidator()
    {
    }
}
