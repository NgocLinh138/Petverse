using FluentValidation;

namespace Contract.Services.V1.Report.Validators
{
    public class UpdateReportValidator : AbstractValidator<Command.UpdateReportCommand>
    {
        public UpdateReportValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái không hợp lệ.");
        }
    }
}
