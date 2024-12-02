using FluentValidation;

namespace Contract.Services.V1.Report.Validators
{
    public class CreateReportValidator : AbstractValidator<Command.CreateReportCommand>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.AppointmentId)
                .NotEmpty().WithMessage("Id cuộc hẹn không được để trống.");
        }
    }
}
