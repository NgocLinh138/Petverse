namespace Contract.Services.V1.Job;

public static class Responses
{
    public record JobResponse
    {
        public Guid Id { get; set; }
        public Guid PetCenterId { get; set; }
        public string Description { get; set; }
        public bool HavePhoto { get; set; }
        public bool HaveCamera { get; set; }
        public bool HaveTransport { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
    }
}
