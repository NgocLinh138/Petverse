namespace Infrastructure.DependencyInjection.Options;
public class JWTOptions
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;

    public string SecretKey { get; set; } = null!;

    public int ExpireMin { get; set; }
}
