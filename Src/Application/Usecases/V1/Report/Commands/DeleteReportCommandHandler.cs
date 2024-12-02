using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Report;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Report.Commands
{
    public sealed class DeleteReportCommandHandler : ICommandHandler<Command.DeleteReportCommand>
    {
        private readonly IReportRepository reportRepository;
        private readonly IUnitOfWork unitOfWork;

        public DeleteReportCommandHandler(
            IReportRepository reportRepository,
            IUnitOfWork unitOfWork)
        {
            this.reportRepository = reportRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(Command.DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var report = await reportRepository.FindByIdAsync(request.Id, cancellationToken);
            if (report == null)
                return Result.Failure("Không tìm thấy báo cáo.", StatusCodes.Status404NotFound);

            reportRepository.Remove(report);
            await unitOfWork.SaveChangesAsync();

            return Result.Success(202);

        }
    }
}
