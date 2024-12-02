using Contract.Services.V1.User;
using FluentValidation;

namespace BHEP.Contract.Services.V1.User.Validators;
internal sealed class CreateUserValidator : AbstractValidator<Command.CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).NotEmpty()
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^[0-9]+$").WithMessage("Phone number must contain only digits.");
        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password Have more than 6 chars");
    }
}
