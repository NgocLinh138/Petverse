using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;
namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetBarChartAdminQueryHandler : IQueryHandler<Query.GetBarChartAdminQuery, Responses.BarChartAdminResponse>
{
    private readonly ITransactionRepository transactionRepository;

    public GetBarChartAdminQueryHandler(ITransactionRepository transactionRepository)
    {
        this.transactionRepository = transactionRepository;
    }

    public async Task<Result<Responses.BarChartAdminResponse>> Handle(Query.GetBarChartAdminQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await transactionRepository.GetMonthlyTransactionSummaryAsync();

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}





