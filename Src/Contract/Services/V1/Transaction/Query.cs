using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;

namespace Contract.Services.V1.Transaction;
public static class Query
{
    public record GetTransactionQuery : IQuery<PagedResult<Responses.TransactionResponse>>
    {
        public Guid? UserId { get; set; }
        public TransactionStatus? Status { get; set; }
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;

    }
    public record GetTransactionByIdQuery(Guid Id) : IQuery<Responses.TransactionResponse>;
}
