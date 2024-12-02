using Contract.Enumerations;
namespace Contract.Services.V1.PetCenterService
{
    public static class Responses
    {
        public record PetCenterServiceResponse
        {
            public int Id { get; init; }
            public Guid PetCenterId { get; init; }
            public int PetServiceId { get; init; }
            public decimal Price { get; init; }
            public float? Rate { get; init; }
            public string? Description { get; init; }
            public ServiceType Type { get; set; }
            public IEnumerable<ScheduleService> Schedule { get; set; } = Enumerable.Empty<ScheduleService>();
        }

        public record ScheduleService(
            string Time,
            string Description);
    }
}
