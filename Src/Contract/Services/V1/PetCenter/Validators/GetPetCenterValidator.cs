using FluentValidation;

namespace Contract.Services.V1.PetCenter.Validators;

public class GetPetCenterValidator : AbstractValidator<Query.GetPetCenterQuery>
{
    public GetPetCenterValidator()
    {
        //RuleFor(x => x.PetServiceId)
        //    .Must(x => x == null || x > 0)
        //    .WithMessage("PetServiceId > 0 if provided.");

        //RuleFor(x => x.PetTypeId)
        //    .Must(x => x == null || x > 0)
        //    .WithMessage("PetTypeId > 0 if provided.");

        // Validate PageIndex and PageSize
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("PageIndex > 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize > 0.");
    }
}
