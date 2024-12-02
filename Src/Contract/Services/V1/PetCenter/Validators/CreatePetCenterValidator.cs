using FluentValidation;

namespace Contract.Services.V1.PetCenter.Validators;

public class CreatePetCenterValidator : AbstractValidator<Command.CreatePetCenterCommand>
{
    public CreatePetCenterValidator()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^[0-9]{8,15}$").WithMessage("Phone number must be valid and contain 8 to 15 digits.");

        RuleFor(command => command.ListServiceIds)
            .NotEmpty().WithMessage("At least one service ID is required.")
            .Must(ids => ids.All(id => id > 0)).WithMessage("All service Id > 0.");
    }
}
