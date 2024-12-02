using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Usecases.V1.Appointment.Commands;

public sealed class UpdateStatusCommandHandler : ICommandHandler<Command.UpdateStatusCommand>
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly ITransactionRepository transactionRepository;
    public UpdateStatusCommandHandler(
        IAppointmentRepository appointmentRepository,
        UserManager<AppUser> userManager,
        IPetCenterRepository petCenterRepository,
        ITransactionRepository transactionRepository)
    {
        this.appointmentRepository = appointmentRepository;
        this.userManager = userManager;
        this.petCenterRepository = petCenterRepository;
        this.transactionRepository = transactionRepository;
    }

    public async Task<Result> Handle(Command.UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(request.Id, includeProperties: x => x.User);
        if (appointment == null)
        {
            return Result.Failure("Không tìm thấy cuộc hẹn.", StatusCodes.Status404NotFound);
        }

        if (!IsValidStatusTransition(appointment.Status, request.Status))
        {
            return Result.Failure("Trạng thái cập nhập không hợp lê.", StatusCodes.Status400BadRequest);
        }
        var nameDescription = GetDescription(appointment);

        var result = request.Status switch
        {
            AppointmentStatus.Received => await HandleReceivedStatusAsync(appointment, nameDescription),
            AppointmentStatus.Refused => HandleRefusedStatus(appointment, request.CancelReason),
            AppointmentStatus.Completed => await HandleCompletedStatusAsync(appointment, nameDescription),
            _ => Result.Success()
        };

        if (!result.IsSuccess)
        {
            return result;
        }

        appointment.Status = request.Status;

        return Result.Success();
    }

    private bool IsValidStatusTransition(AppointmentStatus currentStatus, AppointmentStatus newStatus)
    {
        return currentStatus switch
        {
            AppointmentStatus.Waiting => newStatus == AppointmentStatus.Received || newStatus == AppointmentStatus.Refused,
            AppointmentStatus.Received => newStatus == AppointmentStatus.Completed,
            _ => false
        };
    }

    private async Task<Result> HandleReceivedStatusAsync(Domain.Entities.Appointment appointment, string nameDescription)
    {
        var user = await userManager.FindByIdAsync(appointment.UserId.ToString());
        if (user == null || user.Balance < appointment.Amount)
        {
            return Result.Failure("Người đặt không đủ số dư.", StatusCodes.Status400BadRequest);
        }

        user.Balance -= appointment.Amount;

        var transaction = new Domain.Entities.Transaction
        {
            UserId = appointment.UserId,
            Status = TransactionStatus.Completed,
            Amount = appointment.Amount,
            AppointmentId = appointment.Id,
            Title = TransactionTitle.TruTien,
            Description = (appointment.Type == AppointmentType.ServiceAppointment)
            ? $"Đặt {nameDescription} thành công."
            : $"Đặt phối giống {nameDescription} thành công",
            IsMinus = true,
            OrderCode = 0
        };

        await transactionRepository.AddAsync(transaction);
        return Result.Success();
    }

    private Result HandleRefusedStatus(Domain.Entities.Appointment appointment, string? cancelReason)
    {
        appointment.CancelReason = cancelReason;
        return Result.Success();
    }

    private async Task<Result> HandleCompletedStatusAsync(Domain.Entities.Appointment appointment, string nameDescription)
    {
        var petCenter = await petCenterRepository.FindByIdAsync(appointment.PetCenterId);
        petCenter.Application.User.Balance += appointment.Amount;

        var transaction = new Domain.Entities.Transaction
        {
            UserId = petCenter.Application.User.Id,
            Status = TransactionStatus.Completed,
            Amount = appointment.Amount,
            AppointmentId = appointment.Id,
            Title = TransactionTitle.CongTien,
            Description = (appointment.Type == AppointmentType.ServiceAppointment)
            ? $"Hoàn Thành {nameDescription}"
            : $"Hoàn Thành Phối Giống {nameDescription}",
            IsMinus = false,
            OrderCode = 0
        };
        await transactionRepository.AddAsync(transaction);
        return Result.Success();
    }

    private string GetDescription(Domain.Entities.Appointment appointment)
    {
        var name = appointment.Type switch
        {
            AppointmentType.ServiceAppointment => GetServiceName(appointment),
            AppointmentType.BreedAppointment => GetCenterBreedName(appointment),
            _ => throw new InvalidOperationException($"Loại cuộc hẹn không tồn tại")
        };

        return name;
    }

    private string GetCenterBreedName(Domain.Entities.Appointment appointment)
    {
        if (appointment is not BreedAppointment breedAppointment)
            throw new InvalidOperationException("Cuộc hẹn không phải là phối giống");

        var centerBreedName = breedAppointment.CenterBreed?.Name ?? "Tên không tồn tại";

        return centerBreedName;
    }

    private string GetServiceName(Domain.Entities.Appointment appointment)
    {
        if (appointment is not ServiceAppointment serviceAppointment)
            throw new InvalidOperationException("Cuộc hẹn không phải là dịch vụ");

        var serviceName = serviceAppointment.PetCenterService?.PetService?.Name ?? "Tên dịch vụ không tồn tại";

        return serviceName;
    }

}
