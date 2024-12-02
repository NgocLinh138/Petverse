using Contract.Enumerations;

namespace Contract.Services.V1.Schedule
{
    public static class Responses
    {
        public record ScheduleResponse
        {
            public int ScheduleId { get; set; }
            public List<TrackingResponse> Trackings { get; set; } = new();
        }

        public record TrackingResponse
        {
            public int Id { get; set; }
            public string URL { get; set; } 
            public MediaType Type { get; set; }
            public string UploadedAt { get; set; }
        }
    }
}
