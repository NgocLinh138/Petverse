using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Behaviors;
public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork _unitOfWork; // SQL-SERVER-STRATEGY-2
    private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1

    public TransactionPipelineBehavior(IUnitOfWork unitOfWork, ApplicationDbContext context)
    {
        _unitOfWork = unitOfWork;
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        //// Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //// https://learn.mi crosoft.com/ef/core/miscellaneous/connection-resiliency
        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync(); // Isolation Level here
            {
                try
                {
                    var response = await next();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return response;
                }
                catch (DbUpdateException dbEx)
                {
                    throw new DbUpdateException(dbEx.InnerException?.Message);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        });
    }

    private bool IsCommand()
        => typeof(TRequest).Name.Contains("Command");
}
