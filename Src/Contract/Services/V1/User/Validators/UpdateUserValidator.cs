using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.User.Validators;

internal sealed class UpdateUserValidator : AbstractValidator<Command.UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().NotNull().WithMessage("Id is required");
        RuleFor(x => x.Gender)
            .IsInEnum();
        RuleFor(x => x.Avatar)
            .Must(BeAValidImage)
            .When(x => x.Avatar != null)
            .WithMessage("Avatar must be a valid image file and less than 5MB.");
    }

    private bool BeAValidImage(IFormFile? avatar)
    {
        return avatar != null && avatar.Length <= 5 * 1024 * 1024; // 5MB
    }
}
