using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Exceptions.Abstractions;
using Contract.JsonConverters;
using Contract.Services.V1.Report;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Infrastructure.BlobStorage.Services.Abstraction;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Persistence.Repositories;

namespace Application.Usecases.V1.Report.Commands
{
    public sealed class CreateReportCommandHandler : ICommandHandler<Command.CreateReportCommand, Responses.ReportResponse>
    {
        private readonly IReportRepository reportRepository;
        private readonly IAppointmentRepository appointmentRepository;
        private readonly IReportImageRepository reportImageRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateReportCommandHandler(
            IReportRepository reportRepository,
            IAppointmentRepository appointmentRepository,
            IReportImageRepository reportImageRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.reportRepository = reportRepository;
            this.appointmentRepository = appointmentRepository;
            this.reportImageRepository = reportImageRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.ReportResponse>> Handle(Command.CreateReportCommand request, CancellationToken cancellationToken)
        {
            var appointment = await appointmentRepository.FindByIdAsync(request.AppointmentId, cancellationToken);
            if (appointment == null)
                return Result.Failure<Responses.ReportResponse>("Không tìm thấy cuộc hẹn.", StatusCodes.Status404NotFound);

            if (appointment.Status != AppointmentStatus.Received && appointment.Status != AppointmentStatus.Completed)
                return Result.Failure<Responses.ReportResponse>("Báo cáo chỉ có thể được tạo cho cuộc hẹn đã được nhận hoặc đã hoàn thành.", StatusCodes.Status400BadRequest);

            var existingReport = await reportRepository.FindByAppointmentIdAsync(request.AppointmentId, cancellationToken);
            if (existingReport != null)
                return Result.Failure<Responses.ReportResponse>("Báo cáo đã tồn tại cho cuộc hẹn này.", StatusCodes.Status400BadRequest);
            
            var report = new Domain.Entities.Report
            {
                AppointmentId = request.AppointmentId,
                Title = request.Title,
                Reason = request.Reason,
                Status = ReportStatus.Processing
            };

            await reportRepository.AddAsync(report);
            await unitOfWork.SaveChangesAsync();

            var reportImages = await AddMediaFiles(request, report.Id);

            var response = mapper.Map<Responses.ReportResponse>(report);

            return Result.Success(response, 201);
        }

        private async Task<List<Responses.ReportImageResponse>> AddMediaFiles(Command.CreateReportCommand request, int reportId)
        {
            var trackingResponses = new List<Responses.ReportImageResponse>();
            var random = new Random();

            // Handle images
            if (request.Photos != null && request.Photos.Any())
            {
                foreach (var photo in request.Photos)
                {
                    if (!IsImageByExtension(photo))
                        throw new BadRequestException("Ảnh tải lên phải có định dạng hợp lệ (jpg, jpeg, png).");

                    var fileName = $"{reportId}-photo-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(photo.FileName)}";
                    var url = await blobStorageService.UploadBlob(photo, fileName, BlobFolder.reports);

                    var report = new ReportImage
                    {
                        ReportId = reportId,
                        Type = MediaType.Image,
                        URL = url
                    };

                    await reportImageRepository.AddAsync(report);
                    trackingResponses.Add(new Responses.ReportImageResponse
                    {
                        Id = report.Id,
                        URL = url,
                        Type = MediaType.Image,
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

                    var fileName = $"{reportId}-video-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}{Path.GetExtension(video.FileName)}";
                    var url = await blobStorageService.UploadBlob(video, fileName, BlobFolder.reports);

                    var report = new ReportImage
                    {
                        ReportId = reportId,
                        Type = MediaType.Video,
                        URL = url
                    };

                    await reportImageRepository.AddAsync(report);
                    trackingResponses.Add(new Responses.ReportImageResponse
                    {
                        Id = report.Id,
                        URL = url,
                        Type = MediaType.Video,
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
