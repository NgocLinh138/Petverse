using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Appointment;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Appointment.Queries;

public sealed class GetAppointmentQueryHandler : IQueryHandler<Query.GetAppointmentQuery, PagedResult<Responses.AppointmentResponse>>
{
    private readonly IAppointmentRepository appointmentRepository;
    private readonly IMapper mapper;

    public GetAppointmentQueryHandler(
        IAppointmentRepository appointmentRepository,
        IMapper mapper)
    {
        this.appointmentRepository = appointmentRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.AppointmentResponse>>> Handle(Query.GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        var query = GetAppointmentsQuery(request);

        query = query.OrderByDescending(x => x.CreatedDate);

        var Events = await PagedResult<Domain.Entities.Appointment>.CreateAsync(
                query,
                request.PageIndex,
                request.PageSize);

        var result = mapper.Map<PagedResult<Responses.AppointmentResponse>>(Events);
        return Result.Success(result);
    }

    private IQueryable<Domain.Entities.Appointment> GetAppointmentsQuery(Query.GetAppointmentQuery request)
    {
        var EventsQuery = request.Type.HasValue
            ? appointmentRepository.FindAll(x => x.Type == request.Type)
            : appointmentRepository.FindAll().Include(x => x.User);


        if (request.UserId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.UserId == request.UserId);

        if (request.Status.HasValue)
            EventsQuery = EventsQuery.Where(x => x.Status == request.Status);

        if (request.PetCenterId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.PetCenterId == request.PetCenterId);

        return EventsQuery.Include(x => x.User)
            .Include(x => x.PetCenter)
            .ThenInclude(x => x.Application);
    }

}
