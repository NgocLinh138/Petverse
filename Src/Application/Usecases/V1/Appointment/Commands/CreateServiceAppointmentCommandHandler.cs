using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.JsonConverters;
using Contract.Services.V1.Appointment;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Appointment.Commands;

public sealed class CreateServiceAppointmentCommandHandler : ICommandHandler<Command.CreateServiceAppointmentCommand, Responses.ServiceAppointmentResponse>
{
    private readonly IServiceAppointmentRepository ServiceAppointmentRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    private readonly IPetRepository petRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IScheduleRepository scheduleRepository;
    private const string DateFormat = "dd/MM/yyyy HH:mm";
    public CreateServiceAppointmentCommandHandler(
        IServiceAppointmentRepository ServiceAppointmentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IPetCenterServiceRepository petCenterServiceRepository,
        IPetRepository petRepository,
        IScheduleRepository scheduleRepository)

    {
        this.ServiceAppointmentRepository = ServiceAppointmentRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.petCenterServiceRepository = petCenterServiceRepository;
        this.petRepository = petRepository;
        this.scheduleRepository = scheduleRepository;
    }

    public async Task<Result<Responses.ServiceAppointmentResponse>> Handle(Command.CreateServiceAppointmentCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result.Failure<Responses.ServiceAppointmentResponse>("Không tìm thấy người dùng", StatusCodes.Status404NotFound);


        if (!user.Role.Name.Equals(RoleName.customer, StringComparison.OrdinalIgnoreCase))
            return Result.Failure<Responses.ServiceAppointmentResponse>("Khác hàng mới thể dùng tính năng này", StatusCodes.Status400BadRequest);

        if (!IsEnoughBalance(user, request.Amount))
            return Result.Failure<Responses.ServiceAppointmentResponse>("Tài khoản không đủ số dư", StatusCodes.Status400BadRequest);

        var petCenterService = await petCenterServiceRepository.FindByIdAsync(request.PetCenterServiceId);
        if (petCenterService == null)
            return Result.Failure<Responses.ServiceAppointmentResponse>("Không tìm thấy dịch vụ.", StatusCodes.Status404NotFound);

        var pet = await petRepository.FindSingleAsync(x => x.Id == request.PetId && x.UserId == request.UserId);
        if (pet == null)
            return Result.Failure<Responses.ServiceAppointmentResponse>("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

        if (!await IsSameCenterAsync(request, petCenterService))
            return Result.Failure<Responses.ServiceAppointmentResponse>("Thú cưng đang sử dụng dịch vụ khác.", StatusCodes.Status404NotFound);


        var serviceAppointment = CreateServiceAppointment(request, petCenterService.PetCenterId);

        await ServiceAppointmentRepository.AddAsync(serviceAppointment);
        await unitOfWork.SaveChangesAsync();

        await CreateAppointmentSchedule(serviceAppointment.Id, request);

        var resultResponse = mapper.Map<Responses.ServiceAppointmentResponse>(serviceAppointment);
        return Result.Success(resultResponse, 201);
    }

    private async Task<bool> IsSameCenterAsync(Command.CreateServiceAppointmentCommand request, Domain.Entities.PetCenterService petCenterService)
    {
        var appointment = await ServiceAppointmentRepository
            .FindAll(x => x.UserId == request.UserId
                    && (x.Status == AppointmentStatus.Received || x.Status == AppointmentStatus.Waiting)
                    && x.PetId == request.PetId)
            .FirstOrDefaultAsync();

        if (appointment == null)
            return true;

        if (appointment.PetCenterId == petCenterService.PetCenterId)
            return true;
        return false;

    }

    private Domain.Entities.ServiceAppointment CreateServiceAppointment(Command.CreateServiceAppointmentCommand request, Guid petCenterId)
    {
        return new Domain.Entities.ServiceAppointment
        {
            UserId = request.UserId,
            Type = AppointmentType.ServiceAppointment,
            StartTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).GetValueOrDefault(),
            EndTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).GetValueOrDefault(),
            Status = AppointmentStatus.Waiting,
            PetId = request.PetId,
            PetCenterId = petCenterId,
            PetCenterServiceId = request.PetCenterServiceId,
            Amount = request.Amount,
        };
    }
    private async Task CreateAppointmentSchedule(Guid serviceAppointmentId, Command.CreateServiceAppointmentCommand request)
    {
        var startDateTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).GetValueOrDefault();
        var endDateTime = DateTimeConverters.StringToDate(request.EndTime, DateFormat).GetValueOrDefault();
        var schedules = new List<Domain.Entities.Schedule>();

        for (var date = DateOnly.FromDateTime(startDateTime); date <= DateOnly.FromDateTime(endDateTime); date = date.AddDays(1))
        {
            foreach (var timeEntry in request.Schedules)
            {
                var scheduleDateTime = CombineDateAndTime(date, timeEntry.Time);

                if (scheduleDateTime >= startDateTime && scheduleDateTime <= endDateTime)
                {
                    schedules.Add(new Domain.Entities.Schedule
                    {
                        Date = date,
                        Time = timeEntry.Time,
                        ServiceAppointmentId = serviceAppointmentId,
                        Description = timeEntry.Description,
                    });
                }
            }
        }

        await scheduleRepository.AddMultipleAsync(schedules);
    }
    private DateTime CombineDateAndTime(DateOnly date, string time)
    {
        var scheduleTime = TimeOnly.ParseExact(time, "HH:mm");
        return date.ToDateTime(scheduleTime);
    }
    private bool IsEnoughBalance(AppUser user, decimal amount)
    {
        var totalPendingAmount = user.Appointments
            .Where(x => x.Status == AppointmentStatus.Waiting)
            .Sum(x => x.Amount);

        return user.Balance >= totalPendingAmount + amount;
    }
}

