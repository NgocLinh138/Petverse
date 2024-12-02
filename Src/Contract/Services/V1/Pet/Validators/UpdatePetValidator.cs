using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Pet.Validators
{
    public class UpdatePetValidator : AbstractValidator<Command.UpdatePetCommand>
    {
        public UpdatePetValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id thú cưng không được để trống.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Giới tính không hợp lệ.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Cân nặng phải lớn hơn 0.");

            //RuleFor(x => x.Avatar)
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
