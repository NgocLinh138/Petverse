using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Report;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Application.Usecases.V1.Report.Queries;

public sealed class GetReportQueryHandler : IQueryHandler<Query.GetReportQuery, PagedResult<Responses.ReportResponse>>
{
    private readonly IReportRepository reportRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IMapper mapper;

    public GetReportQueryHandler(
        IReportRepository reportRepository,
        UserManager<AppUser> userManager,
        IMapper mapper)
    {
        this.reportRepository = reportRepository;
        this.userManager = userManager;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.ReportResponse>>> Handle(Query.GetReportQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Report> EventsQuery;
        EventsQuery = string.IsNullOrWhiteSpace(request.SearchTerm)
            ? reportRepository.FindAll()
            : reportRepository.FindAll(x => x.Title.Contains(request.SearchTerm));

        if (request.AppointmentId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.AppointmentId == request.AppointmentId);

        if (request.UserId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.Appointment.UserId == request.UserId);

        if (request.PetCenterId.HasValue)
            EventsQuery = EventsQuery.Where(x => x.Appointment.PetCenterId == request.PetCenterId);

        var keySelector = GetSortProperty(request);

        var sortOrder = (request.SortOrder == 2) ? SortOrder.Descending : SortOrder.Ascending;

        EventsQuery = sortOrder == SortOrder.Descending
            ? EventsQuery.OrderByDescending(keySelector)
            : EventsQuery.OrderBy(keySelector);

        var Events = await PagedResult<Domain.Entities.Report>.CreateAsync(
            EventsQuery,
            request.PageIndex,
            request.PageSize);

        var result = mapper.Map<PagedResult<Responses.ReportResponse>>(Events);

        return Result.Success(result);
    }

    public static Expression<Func<Domain.Entities.Report, object>> GetSortProperty(Query.GetReportQuery request)
        => request.SortColumn?.ToLower() switch
        {
            "title" => e => e.Title,
            _ => e => e.Id
        };
}
