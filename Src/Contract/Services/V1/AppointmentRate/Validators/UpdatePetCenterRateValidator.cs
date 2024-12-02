using Contract.Services.V1.AppointmentRate;
using FluentValidation;

namespace Contract.Services.V1.PetSitterRate.Validators
{
    public class UpdatePetCenterRateValidator : AbstractValidator<Command.UpdateAppointmentRateCommand>
    { 
        public UpdatePetCenterRateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("RateId không được để trống.");

            RuleFor(x => x.Rate)
               .InclusiveBetween(1, 5).WithMessage("Đánh giá phải nằm trong khoảng từ 1 đến 5.");

            RuleFor(x => x.Description)
               .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự.")
               .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
