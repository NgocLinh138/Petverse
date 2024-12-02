//using Contract.Abstractions.Message;
//using Contract.Abstractions.Shared;
//using Contract.Constants;
//using Contract.JsonConverters;
//using Contract.Services.V1.Transaction;
//using Domain.Abstractions;
//using Domain.Abstractions.Repositories;
//using Domain.Entities.Identity;
//using Infrastructure.PayOS.Service.IService;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Net.payOS.Types;
//namespace Application.Usecases.V1.Transaction.Commands;
//public sealed class CreateAppointmentTransactionCommandHandler : ICommandHandler<Command.CreateAppointmentTransactionCommand, Responses.CreateTransactionResponse>
//{
//    private readonly IPayOSService payOSService;
//    private readonly ITransactionRepository transactionRepository;
//    private readonly UserManager<AppUser> userManager;
//    private readonly IUnitOfWork unitOfWork;
//    public CreateAppointmentTransactionCommandHandler(
//        ITransactionRepository transactionRepository,
//        IUnitOfWork unitOfWork,
//        IPayOSService payOSService,
//        UserManager<AppUser> userManager)
//    {
//        this.transactionRepository = transactionRepository;
//        this.unitOfWork = unitOfWork;
//        this.payOSService = payOSService;
//        this.userManager = userManager;
//    }

//    public async Task<Result<Responses.CreateTransactionResponse>> Handle(Command.CreateAppointmentTransactionCommand request, CancellationToken cancellationToken)
//    {
//        // Check User
//        var user = await userManager.FindByIdAsync(request.UserId.ToString());
//        if (user == null)
//            return Result.Failure<Responses.CreateTransactionResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

//        // Check if transaction already exists and completed
//        var existTransaction = await transactionRepository.FindSingleAsync(x => x.AppointmentId == request.AppointmentId);
//        if (existTransaction != null && existTransaction.Status == Contract.Enumerations.TransactionStatus.Completed)
//            return Result.Failure<Responses.CreateTransactionResponse>("Cuộc Hẹn đã giao dịch.", StatusCodes.Status400BadRequest);

//        // If transaction exists but not completed, delete it to recreate
//        if (existTransaction != null)
//        {
//            transactionRepository.Remove(existTransaction);
//            await unitOfWork.SaveChangesAsync();
//        }


//        // Create new transaction
//        var orderCode = GetTimestamp(TimeZones.GetSoutheastAsiaTime());

//        var transaction = Domain.Entities.Transaction.CreateAppointmentTransaction(request, orderCode);


//        // Add new Payment to retrieve ID
//        await transactionRepository.AddAsync(transaction);
//        await unitOfWork.SaveChangesAsync(cancellationToken);

//        // Prepare PayOS Payment Data
//        var expiredAtLong = new DateTimeOffset(DateTime.Now.AddMinutes(20)).ToUnixTimeSeconds();
//        var expiredAt = (int)expiredAtLong;

//        var paymentData = new PaymentData(
//              orderCode,
//              request.Amount,
//              request.Description,
//              request.items,
//              request.cancelUrl,
//              request.returnUrl,
//              expiredAt: expiredAt);
//        try
//        {
//            var payOSResult = await payOSService.CreatePaymentLink(paymentData);

//            var response = CreateResponse(transaction, payOSResult);

//            return response;
//        }
//        catch (Exception)
//        {
//            await payOSService.CancelPaymentLink(orderCode, "Lỗi hệ thống khi tạo Thanh Toán.");
//            throw;
//        }
//    }


//    private static Responses.CreateTransactionResponse CreateResponse(Domain.Entities.Transaction payment, CreatePaymentResult result)
//    {
//        return new Responses.CreateTransactionResponse(
//            payment.Id,
//            payment.UserId,
//            payment.OrderCode,
//            payment.IsMinus,
//            payment.Title,
//            payment.Description,
//            payment.Amount,
//            payment.Status,
//            DateTimeConverters.DateToString(payment.CreatedDate),
//            result.accountNumber,
//            result.currency,
//            result.paymentLinkId,
//            result.status,
//            result.checkoutUrl,
//            result.qrCode);
//    }

//    private static int GetTimestamp(DateTime value)
//    {
//        var code = value.ToString("mmssffff");
//        return int.Parse(code);
//    }
//}
