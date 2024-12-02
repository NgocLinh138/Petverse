using FluentValidation;

namespace Contract.Services.V1.PetCenter.Validators;

public class UpdatePetCenterValidator : AbstractValidator<Command.UpdatePetCenterCommand>
{
    public UpdatePetCenterValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^[0-9]{8,15}$").WithMessage("Phone number must be valid and contain 8 to 15 digits.");
    }
}
