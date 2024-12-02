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

namespace Application.Usecases.V1.Appointment.Commands;

public sealed class CreateBreedAppointmentCommandHandler : ICommandHandler<Command.CreateBreedAppointmentCommand, Responses.BreedAppointmentResponse>
{
    private readonly IBreedAppointmentRepository BreedAppointmentRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly ICenterBreedRepository centerBreedRepository;
    private readonly IPetRepository petRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private const string DateFormat = "dd/MM/yyyy HH:mm";

    public CreateBreedAppointmentCommandHandler(
        IBreedAppointmentRepository BreedAppointmentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        UserManager<AppUser> userManager,
        IPetRepository petRepository,
        ICenterBreedRepository centerBreedRepository)

    {
        this.BreedAppointmentRepository = BreedAppointmentRepository;
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.petRepository = petRepository;
        this.centerBreedRepository = centerBreedRepository;
    }

    public async Task<Result<Responses.BreedAppointmentResponse>> Handle(Command.CreateBreedAppointmentCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null)
            return Result.Failure<Responses.BreedAppointmentResponse>("Không tìm thấy người dùng", StatusCodes.Status404NotFound);

        if (!user.Role.Name.Equals(RoleName.customer, StringComparison.OrdinalIgnoreCase))
            return Result.Failure<Responses.BreedAppointmentResponse>("Khách hàng mới có thể dùng tính năng này.", StatusCodes.Status400BadRequest);

        if (!IsEnoughBalance(user, request.Amount))
            return Result.Failure<Responses.BreedAppointmentResponse>("Tài khoản không đủ số dư", StatusCodes.Status400BadRequest);

        var centerBreed = await centerBreedRepository.FindByIdAsync(request.CenterBreedId);
        if (centerBreed == null)
            return Result.Failure<Responses.BreedAppointmentResponse>("Không tìm thấy giống thú cưng.", StatusCodes.Status404NotFound);

        var pet = await petRepository.FindSingleAsync(x => x.Id == request.PetId && x.UserId == request.UserId);
        if (pet == null)
            return Result.Failure<Responses.BreedAppointmentResponse>("Không tìm thấy thú cưng.", StatusCodes.Status404NotFound);

        var newBreedAppointment = CreateBreedAppointment(request, centerBreed);

        await BreedAppointmentRepository.AddAsync(newBreedAppointment);
        await unitOfWork.SaveChangesAsync();

        var resultResponse = mapper.Map<Responses.BreedAppointmentResponse>(newBreedAppointment);
        return Result.Success(resultResponse, StatusCodes.Status201Created);
    }

    private Domain.Entities.BreedAppointment CreateBreedAppointment(Command.CreateBreedAppointmentCommand request, Domain.Entities.CenterBreed centerBreed)
    {
        return new Domain.Entities.BreedAppointment
        {
            UserId = request.UserId,
            Type = AppointmentType.BreedAppointment,
            StartTime = DateTimeConverters.StringToDate(request.StartTime, DateFormat).GetValueOrDefault(),
            EndTime = DateTimeConverters.StringToDate(request.EndTime, DateFormat).GetValueOrDefault(),
            Status = AppointmentStatus.Waiting,
            PetId = request.PetId,
            CenterBreedId = centerBreed.Id,
            PetCenterId = centerBreed.PetCenterId,
            Amount = request.Amount,
        };
    }

    private bool IsEnoughBalance(AppUser user, decimal amount)
    {
        var totalPendingAmount = user.Appointments
            .Where(x => x.Status == AppointmentStatus.Waiting)
            .Sum(x => x.Amount);

        return user.Balance >= totalPendingAmount + amount;
    }
}
