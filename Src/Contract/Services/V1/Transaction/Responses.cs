using Contract.Enumerations;

namespace Contract.Services.V1.Transaction;
public static class Responses
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public int OrderCode { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsMinus { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public TransactionStatus Status { get; set; }
        public TransactionResponse(
        Guid id,
        Guid appointmentId,
        Guid userId,
        int orderCode,
        string? title,
        string? description,
        decimal amount,
        bool isMinus,
        string createdDate,
        string? updatedDate,
        TransactionStatus status)
        {
            Id = id;
            AppointmentId = appointmentId;
            UserId = userId;
            OrderCode = orderCode;
            Title = title;
            Description = description;
            Amount = amount;
            IsMinus = isMinus;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            Status = status;
        }
    }
    public class CreateTransactionResponse
    {
        public Guid Id { get; set; }
        public Guid? AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public int OrderCode { get; set; }
        public bool IsMinus { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string CreatedDate { get; set; }
        public string AccountNumber { get; set; }
        public string Currency { get; set; }
        public string PaymentLinkId { get; set; }
        public string PayOSStatus { get; set; }
        public string CheckoutUrl { get; set; }
        public string QRCode { get; set; }

        public CreateTransactionResponse(
            Guid id,
            Guid userId,
            int orderCode,
            bool isMinus,
            string? title,
            string? description,
            decimal amount,
            TransactionStatus transactionStatus,
            string createdDate,
            string accountNumber,
            string currency,
            string paymentLinkId,
            string payOSStatus,
            string checkoutUrl,
            string qrCode)
        {
            Id = id;
            UserId = userId;
            OrderCode = orderCode;
            IsMinus = isMinus;
            Title = title;
            Description = description;
            Amount = amount;
            TransactionStatus = transactionStatus;
            CreatedDate = createdDate;
            AccountNumber = accountNumber;
            Currency = currency;
            PaymentLinkId = paymentLinkId;
            PayOSStatus = payOSStatus;
            CheckoutUrl = checkoutUrl;
            QRCode = qrCode;
        }
    }
}
