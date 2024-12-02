namespace Contract.Services.V1.Role;

public static class Responses
{
    public record RoleResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
    }
}
