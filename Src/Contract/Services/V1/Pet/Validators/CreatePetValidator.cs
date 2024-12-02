using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Pet.Validators
{
    public class CreatePetValidator : AbstractValidator<Command.CreatePetCommand>
    {
        public CreatePetValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Id người dùng không được để trống.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên không được để trống.")
                .MaximumLength(100).WithMessage("Tên không được vượt quá 100 ký tự.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Giới tính không hợp lệ.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Cân nặng phải lớn hơn 0.");

            //RuleFor(x => x.Avatar)
            //    .NotEmpty().WithMessage("Hình ảnh không được để trống.")
            //    .Must(BeAValidImage).WithMessage("Hình ảnh tải lên phải có định dạng hợp lệ (jpg, jpeg, png).")
            //    .When(x => x.Avatar != null);
        }

        //private bool BeAValidImage(IFormFile file)
        //{
        //    if (file == null) return false;

        //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        //    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        //    if (file.Length > 5 * 1024 * 1024)
        //    {
        //        return false;
        //    }

        //    return allowedExtensions.Contains(extension);
        //}
    }
}
