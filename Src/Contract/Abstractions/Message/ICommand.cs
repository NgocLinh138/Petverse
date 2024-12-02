using Contract.Abstractions.Shared;
using MassTransit;
using MediatR;

namespace Contract.Abstractions.Message;
[ExcludeFromTopology]
public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

//public interface IBlobCommand : IRequest<Result>
//{
//    public ICollection<IFormFile> Files { get; set; }
//    public string name { get; set; }
//    public string folder { get; set; }
//}

//public interface IBlobCommand<TResponse> : IRequest<Result<TResponse>>
//{
//    public ICollection<IFormFile> Files { get; set; }
//    public string name { get; set; }
//    public string folder { get; set; }
//}

