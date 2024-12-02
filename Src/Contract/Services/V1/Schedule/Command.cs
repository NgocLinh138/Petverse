using Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace Contract.Services.V1.Schedule
{
    public static class Command
    {
        public record UpdateScheduleCommand : ICommand<Responses.ScheduleResponse> 
        {
            public int Id { get; set; }
            public ICollection<IFormFile>? Photos { get; set; }
            public ICollection<IFormFile>? Videos { get; set; }
        }
    }
}
