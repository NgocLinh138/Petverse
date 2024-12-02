using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Transaction;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.Transaction.Queries;
public sealed class GetTransactionByIdQueryHandler : IQueryHandler<Query.GetTransactionByIdQuery, Responses.TransactionResponse>
{
    private readonly ITransactionRepository transactionRepository;
    private readonly IMapper mapper;

    public GetTransactionByIdQueryHandler(
        ITransactionRepository transactionRepository,
        IMapper mapper)
    {
        this.transactionRepository = transactionRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.TransactionResponse>> Handle(Query.GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await transactionRepository.FindSingleAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        var resultResponse = mapper.Map<Responses.TransactionResponse>(result);
        return Result.Success(resultResponse);
    }
}
