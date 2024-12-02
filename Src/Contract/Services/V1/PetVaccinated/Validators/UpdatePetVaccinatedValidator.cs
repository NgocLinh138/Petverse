using Contract.JsonConverters;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.PetVaccinated.Validators
{
    public class UpdatePetVaccinatedValidator : AbstractValidator<Command.UpdatePetVaccinatedCommand>
    {
        public UpdatePetVaccinatedValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Tên vaccine không được để trống.")
              .MaximumLength(100).WithMessage("Tên vaccine phải dưới 100 ký tự.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Hình ảnh không được để trống.")
                .Must(BeAValidImage).WithMessage("Hình ảnh tải lên phải có định dạng hợp lệ (jpg, jpeg, png).")
                .When(x => x.Image != null);

            RuleFor(x => x.DateVaccinated)
                .NotEmpty().WithMessage("Ngày tiêm vaccine không được để trống.")
                .Must(BeAValidDate).WithMessage("Ngày tiêm vaccine không hợp lệ.")
                .Must(BeInThePastOrToday).WithMessage("Ngày tiêm vaccine phải là ngày hôm nay hoặc trong quá khứ.");
        }

        private bool BeAValidImage(IFormFile file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (file.Length > 5 * 1024 * 1024)
            {
                return false;
            }

            return allowedExtensions.Contains(extension);
        }

        private bool BeAValidDate(string date)
        {
            return DateTimeConverters.StringToDate(date) != null;
        }

        private bool BeInThePastOrToday(string date)
        {
            var parsedDate = DateTimeConverters.StringToDate(date);
            return parsedDate.HasValue && parsedDate.Value.Date <= DateTime.Today;
        }
    }
}
