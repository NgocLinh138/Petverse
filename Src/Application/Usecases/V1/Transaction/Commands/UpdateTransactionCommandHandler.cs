using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Enumerations;
using Contract.Services.V1.Transaction;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Application.Usecases.V1.Transaction.Commands;
public sealed class UpdateTransactionCommandHandler : ICommandHandler<Command.UpdateTransactionCommand>
{
    private readonly IMapper mapper;
    private readonly ITransactionRepository transactionRepository;
    private readonly UserManager<AppUser> userManager;
    public UpdateTransactionCommandHandler(
        IMapper mapper,
        ITransactionRepository transactionRepository,
        UserManager<AppUser> userManager)
    {
        this.mapper = mapper;
        this.transactionRepository = transactionRepository;
        this.userManager = userManager;
    }

    public async Task<Result> Handle(Command.UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = await transactionRepository.FindByIdAsync(request.Id.Value, cancellationToken);
        if (transaction == null)
            return Result.Failure("Không tìm thấy giao dịch.", StatusCodes.Status404NotFound);

        if (transaction.Status != TransactionStatus.Pending)
            return Result.Failure("Giao dịch đã xử lý.", StatusCodes.Status400BadRequest);

        var user = await userManager.FindByIdAsync(transaction.UserId.ToString());
        user.Balance += transaction.Amount;

        transaction.Status = request.Status;

        await userManager.UpdateAsync(user);
        transactionRepository.Update(transaction);

        return Result.Success(202);

    }
}
