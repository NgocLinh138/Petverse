using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetLineChartAdminQueryHandler : IQueryHandler<Query.GetLineChartAdminQuery, Responses.LineChartAdminResponse>
{
    private readonly ITransactionRepository transactionRepository;

    public GetLineChartAdminQueryHandler(ITransactionRepository paymentRepository)
    {
        this.transactionRepository = paymentRepository;
    }

    public async Task<Result<Responses.LineChartAdminResponse>> Handle(Query.GetLineChartAdminQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var PipeChart = await transactionRepository.GetMonthlyServiceAppointmentSummaryAsync();

            return Result.Success(PipeChart);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}


