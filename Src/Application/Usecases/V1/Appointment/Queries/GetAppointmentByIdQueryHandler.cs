using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using static Contract.Services.V1.Appointment.Responses;
using static Contract.Services.V1.Schedule.Responses;

namespace Application.Usecases.V1.Appointment.Queries;

public sealed class GetAppointmentByIdQueryHandler : IQueryHandler<Query.GetAppointmentByIdQuery, Responses.AppointmentByIdResponse>
{
    private readonly IAppointmentRepository AppointmentRepository;
    private readonly IMapper mapper;
    private readonly ApplicationDbContext context;
    public GetAppointmentByIdQueryHandler(
        IAppointmentRepository AppointmentRepository,
        IMapper mapper,
        ApplicationDbContext context)
    {
        this.AppointmentRepository = AppointmentRepository;
        this.mapper = mapper;
        this.context = context;
    }

    public async Task<Result<Responses.AppointmentByIdResponse>> Handle(Query.GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await AppointmentRepository.GetAppointmentById(request.AppointmentId, request.Type);

        if (appointment == null)
            return Result.Failure<Responses.AppointmentByIdResponse>("Không tìm thấy cuộc hẹn.", StatusCodes.Status404NotFound);

        var response = mapper.Map<Responses.AppointmentByIdResponse>(appointment);

        if (appointment is ServiceAppointment serviceAppointment)
        {
            response.Schedules = await GetScheduleAppointmentResponsesAsync(serviceAppointment.Id);
        }

        return Result.Success(response);
    }

    private async Task<List<ScheduleAppointmentResponse>> GetScheduleAppointmentResponsesAsync(Guid appointmentId)
    {
        // Lấy danh sách các lịch trình (Schedules) từ database dựa trên ServiceAppointmentId
        var schedules = await context.Schedule
            .Where(s => s.ServiceAppointmentId == appointmentId)
            .Include(s => s.Trackings)
            .OrderBy(x => x.Date)
            .ToListAsync();

        // Ánh xạ mỗi Schedule thành ScheduleAppointmentResponse
        var scheduleResponses = schedules
            .GroupBy(s => s.Date) // Nhóm lịch trình theo Date
            .Select(group => new ScheduleAppointmentResponse(
                Date: group.Key.ToString("dd/MM/yyyy"),
                Records: group.Select(schedule => new Record(
                    ScheduleId: schedule.Id,
                    Time: schedule.Time,
                    Description: schedule.Description,
                    Trackings: mapper.Map<List<TrackingResponse>>(schedule.Trackings))
                    ).ToList()
            )).ToList();

        return scheduleResponses;
    }
}
