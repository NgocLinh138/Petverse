using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;
namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetBarChartManagerQueryHandler : IQueryHandler<Query.GetBarChartManagerQuery, Responses.BarChartManagerResponse>
{
    private readonly ITransactionRepository transactionRepository;

    public GetBarChartManagerQueryHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task<Result<Responses.BarChartManagerResponse>> Handle(Query.GetBarChartManagerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await transactionRepository.GetMonthlyReportSummaryAsync();

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}





