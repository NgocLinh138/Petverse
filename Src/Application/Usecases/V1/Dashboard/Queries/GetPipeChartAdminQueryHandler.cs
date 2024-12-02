using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetPipeChartAdminQueryHandler : IQueryHandler<Query.GetPipeChartAdminQuery, Responses.PipeChartAdminResponse>
{
    private readonly IReportRepository reportRepository;
    public GetPipeChartAdminQueryHandler(IReportRepository reportRepository)
    {
        this.reportRepository = reportRepository;
    }

    public async Task<Result<Responses.PipeChartAdminResponse>> Handle(Query.GetPipeChartAdminQuery request, CancellationToken cancellationToken)
    {
        try
        {
            IQueryable<Domain.Entities.Report> reports = reportRepository.FindAll();

            int Processing = await reports.CountAsync(x => x.Status == ReportStatus.Processing);
            int Approved = await reports.CountAsync(r => r.Status == ReportStatus.Approved);
            int Rejected = await reports.CountAsync(r => r.Status == ReportStatus.Rejected);

            var response = new Responses.PipeChartAdminResponse(
                Processing,
                Approved,
                Rejected);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}