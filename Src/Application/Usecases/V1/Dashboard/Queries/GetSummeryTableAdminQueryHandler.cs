using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetSummeryTableAdminQueryHandler : IQueryHandler<Query.GetSummeryTableAdminQuery, Responses.SummeryTableAdminResponse>
{
    private readonly ITransactionRepository paymentRepository;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IMapper mapper;
    public GetSummeryTableAdminQueryHandler(
        ITransactionRepository paymentRepository,
        IPetCenterRepository petCenterRepository,
        IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.petCenterRepository = petCenterRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.SummeryTableAdminResponse>> Handle(Query.GetSummeryTableAdminQuery request, CancellationToken cancellationToken)
    {
        try
        {
            // Get 5 Recently Completed  Payment
            var listRecentlyCompletedPayment = paymentRepository.GetRecentlyCompletedTransaction();

            var recentPaymentResponse = listRecentlyCompletedPayment.Select(x =>
                            new Responses.RecentPayment(x.Title, x.CreatedDate, x.Amount, x.Status)).ToList();

            // Get Top Center
            var topcenter = await petCenterRepository.GetTopPetCenterOfService();

            var response = new Responses.SummeryTableAdminResponse(
                    topcenter,
                    recentPaymentResponse);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}


