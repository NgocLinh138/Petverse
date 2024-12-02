using FluentValidation;

namespace Contract.Services.V1.Application.Validators
{
    public class UpdateApplicationStatusValidator : AbstractValidator<Command.UpdateApplicationStatusCommand>
    {
        public UpdateApplicationStatusValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái không hợp lệ (Processing = 1, Approve = 2, Cancel = -1).");
        }
    }
}
