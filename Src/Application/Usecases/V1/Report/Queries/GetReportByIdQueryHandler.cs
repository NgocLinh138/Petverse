using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Report;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Report.Queries;

public sealed class GetReportByIdQueryHandler : IQueryHandler<Query.GetReportByIdQuery, Responses.ReportResponse>
{
    private readonly IReportRepository reportRepository;
    private readonly IMapper mapper;

    public GetReportByIdQueryHandler(
        IReportRepository reportRepository,
        IMapper mapper)
    {
        this.reportRepository = reportRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.ReportResponse>> Handle(Query.GetReportByIdQuery request, CancellationToken cancellationToken)
    {
        var report = await reportRepository.FindByIdAsync(request.Id, cancellationToken);
        if (report == null)
            return Result.Failure<Responses.ReportResponse>("Không tìm thấy báo cáo.", StatusCodes.Status404NotFound);

        var response = mapper.Map<Responses.ReportResponse>(report);

        return Result.Success(response);
    }
}
