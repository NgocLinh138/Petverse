using Infrastructure.PayOS.DependencyInjection.Options;
using Infrastructure.PayOS.Service.IService;
using Microsoft.Extensions.Options;
using Net.payOS.Types;

namespace Infrastructure.PayOS.Service;
public class PayOSService : IPayOSService
{
    private readonly PayOSOptions payOSOptions;
    private readonly Net.payOS.PayOS payOS;
    public PayOSService(IOptions<PayOSOptions> PayOSOptions)
    {
        payOSOptions = PayOSOptions.Value;
        payOS = new Net.payOS.PayOS(
            payOSOptions.ClientID ?? throw new Exception("Cannot find ClientID of PayOS"),
            payOSOptions.APIKey ?? throw new Exception("Cannot find APIKey of PayOS"),
            payOSOptions.CheckSumKey ?? throw new Exception("Cannot find CheckSumKey of PayOS"));
    }

    public async Task<PaymentLinkInformation> GetPaymentLink(int orderId)
        => await payOS.getPaymentLinkInformation(orderId);

    public async Task<CreatePaymentResult> CreatePaymentLink(PaymentData paymentData)
        => await payOS.createPaymentLink(paymentData);

    public async Task<PaymentLinkInformation> CancelPaymentLink(int orderId, string? reason)
        => await payOS.cancelPaymentLink(orderId, reason);

    public async Task<string> ConfirmWebhook(string webhookUrl)
        => await payOS.confirmWebhook(webhookUrl);

    public WebhookData VerifyPaymentWebhookData(WebhookType webhookBody)
        => payOS.verifyPaymentWebhookData(webhookBody);
}
