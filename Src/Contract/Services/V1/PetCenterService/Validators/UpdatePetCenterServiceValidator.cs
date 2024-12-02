using FluentValidation;

namespace Contract.Services.V1.PetCenterService.Validators
{
    public class UpdatePetCenterServiceValidator : AbstractValidator<Command.UpdatePetCenterServiceCommand>
    {
        public UpdatePetCenterServiceValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Giá phải lớn hơn 0.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Loại dịch vụ không hợp lệ.");
        }
    }
}
