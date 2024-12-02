using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Exceptions.Abstractions;
using Contract.JsonConverters;
using Contract.Services.V1.Schedule;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Schedule.Commands
{
    public sealed class UpdateScheduleCommandHandler : ICommandHandler<Command.UpdateScheduleCommand, Responses.ScheduleResponse>
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly ITrackingRepository trackingRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateScheduleCommandHandler(
            IScheduleRepository scheduleRepository,
            ITrackingRepository trackingRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.scheduleRepository = scheduleRepository;
            this.trackingRepository = trackingRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.ScheduleResponse>> Handle(Command.UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await scheduleRepository.FindByIdAsync(request.Id, cancellationToken);
            if (schedule == null)
                return Result.Failure<Responses.ScheduleResponse>("Không tìm thấy lịch trình.", StatusCodes.Status404NotFound);

            var trackings = await AddMediaFiles(request, schedule.Id);
            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.ScheduleResponse>(schedule);

            return Result.Success(response, StatusCodes.Status200OK);
        }

        private async Task<List<Responses.TrackingResponse>> AddMediaFiles(Command.UpdateScheduleCommand request, int scheduleId)
        {
            var trackingResponses = new List<Responses.TrackingResponse>();
            var random = new Random();

            // Handle images
            if (request.Photos != null && request.Photos.Any())
            {
                foreach (var photo in request.Photos)
                {
                    if (!IsImageByExtension(photo))
                        throw new BadRequestException("Ảnh tải lên phải có định dạng hợp lệ (jpg, jpeg, png).");

                    var fileName = $"{scheduleId}-photo-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(photo.FileName)}";
                    var url = await blobStorageService.UploadBlob(photo, fileName, BlobFolder.trackings);

                    var tracking = new Tracking
                    {
                        ScheduleId = scheduleId,
                        Type = MediaType.Image,
                        URL = url
                    };

                    await trackingRepository.AddAsync(tracking);
                    trackingResponses.Add(new Responses.TrackingResponse
                    {
                        Id = tracking.Id,
                        URL = url,
                        Type = MediaType.Image,
                        UploadedAt = DateTimeConverters.DateToString(tracking.UploadedAt)
                    });
                }
            }

            // Handle videos
            if (request.Videos != null && request.Videos.Any())
            {
                foreach (var video in request.Videos)
                {
                    if (!IsVideoByExtension(video))
                        throw new BadRequestException("Video tải lên phải có định dạng hợp lệ (.mp4, .avi, .wmv, .mpeg).");

                    var fileName = $"{scheduleId}-video-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(video.FileName)}";
                    var url = await blobStorageService.UploadBlob(video, fileName, BlobFolder.trackings);

                    var tracking = new Tracking
                    {
                        ScheduleId = scheduleId,
                        Type = MediaType.Video,
                        URL = url
                    };

                    await trackingRepository.AddAsync(tracking);
                    trackingResponses.Add(new Responses.TrackingResponse
                    {
                        Id = tracking.Id,
                        URL = url,
                        Type = MediaType.Video,
                        UploadedAt = DateTimeConverters.DateToString(tracking.UploadedAt)
                    });
                }
            }

            return trackingResponses;
        }

        private bool IsImageByExtension(IFormFile file)
        {
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(extension);
        }

        private bool IsVideoByExtension(IFormFile file)
        {
            var validExtensions = new[] { ".mp4", ".avi", ".wmv", ".mpeg" };
            var extension = Path.GetExtension(file.FileName).ToLower();
            return validExtensions.Contains(extension);
        }
    }
}
