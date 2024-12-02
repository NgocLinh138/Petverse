using Contract.Abstractions.Shared;
using MediatR;

namespace Contract.Abstractions.Message;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
