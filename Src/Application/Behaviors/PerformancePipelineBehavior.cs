using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviors;
public class PerformancePipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> logger;
    private readonly Stopwatch timer;
    public PerformancePipelineBehavior(ILogger<TRequest> logger)
    {
        this.logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        timer.Start();
        var response = await next();
        timer.Stop();

        var timeExecute = timer.ElapsedMilliseconds; //Miliseconds

        if (timeExecute > 5000)
        {
            var requestName = typeof(TRequest).Name;
            logger.LogWarning("Long Time Running - Request Details: {Name} ({timeExecute} milliseconds) {@Request}",
                requestName, timeExecute, request)
;
        }
        return response;
    }
}
