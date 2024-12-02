using System.ComponentModel.DataAnnotations;

namespace Persistence.DependencyInjection.Options;
public class SqlServerRetryOptions
{
    [Required, Range(5, 20)]
    public int MaxRetryCount { get; set; }
    [Required, Timestamp]
    public TimeSpan MaxRetryDelay { get; set; }
    public int[]? ErrorNumbersToAdd { get; set; }

}
