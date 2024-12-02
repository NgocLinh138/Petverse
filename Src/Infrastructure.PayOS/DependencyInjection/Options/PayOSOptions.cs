namespace Infrastructure.PayOS.DependencyInjection.Options;
public class PayOSOptions
{
    public string ClientID { get; set; } = null!;
    public string APIKey { get; set; } = null!;
    public string CheckSumKey { get; set; } = null!;
}
