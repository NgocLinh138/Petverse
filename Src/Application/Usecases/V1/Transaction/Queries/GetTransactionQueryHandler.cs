using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Transaction;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Transaction.Queries;

public sealed class GetTransactionQueryHandler : IQueryHandler<Query.GetTransactionQuery, PagedResult<Responses.TransactionResponse>>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetTransactionQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<PagedResult<Responses.TransactionResponse>>> Handle(Query.GetTransactionQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var query = GetPaymentsQuery(request);

            // Sort
            query = query.OrderByDescending(x => x.CreatedDate);

            var Events = await PagedResult<Domain.Entities.Transaction>.CreateAsync(
                query,
                request.PageIndex,
                request.PageSize);

            var result = mapper.Map<PagedResult<Responses.TransactionResponse>>(Events);

            return Result.Success(result);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private IQueryable<Domain.Entities.Transaction> GetPaymentsQuery(Query.GetTransactionQuery request)
    {
        var query = request.UserId.HasValue
            ? transactionRepository.FindAll(x => x.UserId == request.UserId)
            : transactionRepository.FindAll();

        if (request.Status.HasValue)
            query = query.Where(x => x.Status == request.Status);

        return query;
    }
}
