using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Report;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Net.payOS.Types;
using Persistence.Repositories;

namespace Application.Usecases.V1.Report.Commands
{
    public sealed class UpdateReportCommandHandler : ICommandHandler<Command.UpdateReportCommand, Responses.ReportResponse>
    {
        private readonly IReportRepository reportRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IPetCenterRepository petCenterRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateReportCommandHandler(
            IReportRepository reportRepository,
            IAppointmentRepository appointmentRepository,
            IPetCenterRepository petCenterRepository,
            ITransactionRepository transactionRepository,
            UserManager<AppUser> userManager,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.reportRepository = reportRepository;
            this.appointmentRepository = appointmentRepository;
            this.petCenterRepository = petCenterRepository;
            this.transactionRepository = transactionRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.ReportResponse>> Handle(Command.UpdateReportCommand request, CancellationToken cancellationToken)
        {
            var report = await reportRepository.FindByIdAsync(request.Id, cancellationToken);
            if (report == null)
                return Result.Failure<Responses.ReportResponse>("Không tìm thấy báo cáo.", StatusCodes.Status404NotFound);

            if (report.Status != ReportStatus.Processing)
                return Result.Failure<Responses.ReportResponse>("Báo cáo đã được xử lý.", StatusCodes.Status400BadRequest);

            report.Update(request);
            reportRepository.Update(report);
            await unitOfWork.SaveChangesAsync();

            if (report.Status == ReportStatus.Approved)
            {
                var appointment = await appointmentRepository.FindByIdAsync(report.AppointmentId, cancellationToken);
                if (appointment != null)
                {
                    appointment.IsReported = true;
                    appointment.UpdatedDate = TimeZones.GetSoutheastAsiaTime();
                    appointmentRepository.Update(appointment);

                    var petCenter = await petCenterRepository.FindByIdAsync(appointment.PetCenterId, cancellationToken);
                    if (petCenter != null)
                    {
                        if (appointment.Status == AppointmentStatus.Received)
                        {
                            // Nếu trạng thái Appointment là "Received", chỉ hoàn tiền cho User mà không trừ tiền từ PetCenter
                            var refundTransaction = Domain.Entities.Transaction.CreateAddBalanceTransaction(
                                new Contract.Services.V1.Transaction.Command.CreateAddBalanceTransactionCommand(
                                    UserId: appointment.UserId,
                                    Title: "Hoàn Tiền",
                                    Description: $"Hoàn tiền cho cuộc hẹn",
                                    Amount: (int)appointment.Amount,
                                    returnUrl: string.Empty,
                                    cancelUrl: string.Empty),
                                GetTimestamp(TimeZones.GetSoutheastAsiaTime())
                            );

                            await transactionRepository.AddAsync(refundTransaction);

                            var user = await userManager.FindByIdAsync(appointment.UserId.ToString());
                            user.Balance += appointment.Amount;
                            await userManager.UpdateAsync(user);
                        }
                        else if (appointment.Status == AppointmentStatus.Completed)
                        {
                            // Nếu trạng thái Appointment là "Completed", hoàn tiền cho User và trừ tiền từ PetCenter
                            var refundTransaction = Domain.Entities.Transaction.CreateAddBalanceTransaction(
                                new Contract.Services.V1.Transaction.Command.CreateAddBalanceTransactionCommand(
                                    UserId: appointment.UserId,
                                    Title: "Hoàn Tiền",
                                    Description: "Hoàn tiền cho cuộc hẹn",
                                    Amount: (int)appointment.Amount,
                                    returnUrl: string.Empty,
                                    cancelUrl: string.Empty),
                                GetTimestamp(TimeZones.GetSoutheastAsiaTime())
                            );

                            await transactionRepository.AddAsync(refundTransaction);

                            var user = await userManager.FindByIdAsync(appointment.UserId.ToString());
                            user.Balance += appointment.Amount;
                            await userManager.UpdateAsync(user);
                            await unitOfWork.SaveChangesAsync();

                            refundTransaction.Status = TransactionStatus.Completed;
                            transactionRepository.Update(refundTransaction);


                            // Tạo transaction trừ tiền Pet Center
                            var deductTransaction = Domain.Entities.Transaction.CreateAppointmentTransaction(
                                new Contract.Services.V1.Transaction.Command.CreateAppointmentTransactionCommand(
                                    UserId: petCenter.Application.UserId,
                                    AppointmentId: appointment.Id,
                                    Title: "Trừ Tiền",
                                    Description: "Trừ tiền cho cuộc hẹn",
                                    Amount: (int)appointment.Amount,
                                    items: new List<ItemData>(),
                                    returnUrl: string.Empty,
                                    cancelUrl: string.Empty),
                                GetTimestamp(TimeZones.GetSoutheastAsiaTime())
                            );

                            deductTransaction.IsMinus = true;
                            await transactionRepository.AddAsync(deductTransaction);
                            await unitOfWork.SaveChangesAsync();

                            deductTransaction.Status = TransactionStatus.Completed;
                            transactionRepository.Update(deductTransaction);

                            petCenter.Application.User.Balance -= appointment.Amount;
                            petCenter.NumReported += 1;

                            if (petCenter.NumReported >= 3)
                            {
                                petCenter.IsDisabled = true;
                            }

                            petCenter.UpdatedDate = TimeZones.GetSoutheastAsiaTime();
                        }

                        petCenterRepository.Update(petCenter);
                    }
                }
            }

            var response = mapper.Map<Responses.ReportResponse>(report);

            return Result.Success(response, 201);
        }

        
        private static int GetTimestamp(DateTime value)
        {
            var code = value.ToString("mmssffff");
            return int.Parse(code);
        }
    }
}
