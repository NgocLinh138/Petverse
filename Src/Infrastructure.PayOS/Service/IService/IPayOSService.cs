using Net.payOS.Types;

namespace Infrastructure.PayOS.Service.IService;
public interface IPayOSService
{
    Task<PaymentLinkInformation> GetPaymentLink(int orderId);
    Task<CreatePaymentResult> CreatePaymentLink(PaymentData paymentData);
    Task<PaymentLinkInformation> CancelPaymentLink(int orderId, string? reason);
    Task<string> ConfirmWebhook(string webhookUrl);
    WebhookData VerifyPaymentWebhookData(WebhookType webhookBody);
}
