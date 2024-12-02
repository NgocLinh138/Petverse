namespace Infrastructure.BlobStorage.DependencyInjection.Options;
public class BlobStorageOptions
{
    public string ConnectionString { get; set; } = null!;
    public string AccountName { get; set; } = null!;
    public string Container { get; set; } = null!;
    public string AccountKey { get; set; } = null!;
}
