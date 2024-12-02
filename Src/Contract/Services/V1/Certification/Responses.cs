namespace Contract.Services.V1.Certification
{
    public static class Responses
    {
        public record CertificationResponse
        {
            public int Id { get; set; }
            public int ApplicationId { get; set; }
            public string Image { get; set; } = null!;
        }
    }
}
