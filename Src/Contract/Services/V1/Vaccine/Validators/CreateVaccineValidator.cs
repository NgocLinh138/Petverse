using FluentValidation;

namespace Contract.Services.V1.Vaccine.Validators
{
    public class CreateVaccineValidator : AbstractValidator<Command.CreateVaccineCommand>
    {
        public CreateVaccineValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên vaccine không được để trống.")
                .MaximumLength(100).WithMessage("Tên vaccine không được vượt quá 100 ký tự.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.MinAge)
                .NotEmpty().WithMessage("Tháng tuổi không được để trống.")
                .GreaterThanOrEqualTo(0).WithMessage("Tháng tuổi phải lớn hơn hoặc bằng 0");
        }
    }
}
