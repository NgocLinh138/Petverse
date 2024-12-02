namespace Contract.Services.V1.AppointmentRate
{
    public static class Responses
    {
        public record AppointmentRateResponse
        {
            public int Id { get; init; }
            public Guid AppointmentId { get; init; }
            public string UserName { get; init; }
            public string Avatar { get; init; }
            public string CreatedDate { get; init; }
            public float Rate { get; init; }
            public string? Description { get; init; }
        }


    }
}
