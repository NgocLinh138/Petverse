using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetLineChartManagerQueryHandler : IQueryHandler<Query.GetLineChartManagerQuery, Responses.LineChartManagerResponse>
{
    private readonly ITransactionRepository transactionRepository;

    public GetLineChartManagerQueryHandler(ITransactionRepository paymentRepository)
    {
        this.transactionRepository = paymentRepository;
    }

    public async Task<Result<Responses.LineChartManagerResponse>> Handle(Query.GetLineChartManagerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var PipeChart = await transactionRepository.GetMonthlyPetAndPetCenterSummaryAsync();

            return Result.Success(PipeChart);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}


