using Contract.Abstractions.Shared;
using MassTransit;
using MediatR;

namespace Contract.Abstractions.Message;

[ExcludeFromTopology]
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}
[ExcludeFromTopology]
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}
