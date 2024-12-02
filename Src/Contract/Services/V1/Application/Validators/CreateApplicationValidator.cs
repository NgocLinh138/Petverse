using FluentValidation;

namespace Contract.Services.V1.Application.Validators
{
    public class CreateApplicationValidator : AbstractValidator<Command.CreateApplicationCommand>
    {
        public CreateApplicationValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Tên không được để trống.")
               .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^[0-9]+$").WithMessage("Số điện thoại chỉ được chứa các chữ số.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.");

            RuleFor(X => X.PetServiceIds)
                .Must(ids => ids.All(id => id > 0)).WithMessage("Tất cả Id dịch vụ phải lớn hơn 0.");
        }
    }
}
