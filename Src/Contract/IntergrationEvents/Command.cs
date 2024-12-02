using Contract.Abstractions.Message;

namespace Contract.IntergrationEvents;

public static class Command
{
    public record class SendNotification : INotification
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Type { get; set; } = null!;
        public Guid TransactionId { get; set; }
    }
}
