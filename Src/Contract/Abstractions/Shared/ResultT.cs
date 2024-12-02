using Contract.Exceptions.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Contract.Abstractions.Shared;
public class Result<TData> : Result
{
    private readonly TData? _data;
    public Result(TData? data, bool isSuccess, string error, int statusCode = 200)
        : base(isSuccess, error, statusCode) =>
        _data = data;
    public TData Data => IsSuccess
        ? _data!
        : string.IsNullOrEmpty(Message)
        ? throw new InvalidOperationException("The data of a failure result can not be accessed.")
        : StatusCode switch
        {
            401 => throw new UnauthorizedException(Message),
            403 => throw new ForbiddenException(Message),
            404 => throw new NotFoundException(Message),
            _ => throw new BadHttpRequestException(Message)
        };


    public static implicit operator Result<TData>(TData? data) => Create(data);
}
