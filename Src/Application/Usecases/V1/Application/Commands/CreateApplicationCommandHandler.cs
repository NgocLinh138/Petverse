using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Enumerations;
using Contract.Services.V1.Application;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities.Identity;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Contract.Services.V1.Application.Responses;

namespace Application.Usecases.V1.Application.Commands;

public sealed class CreateApplicationCommandHandler : ICommandHandler<Command.CreateApplicationCommand, Responses.ApplicationResponse>
{
    private readonly IApplicationRepository applicationRepository;
    private readonly ICertificationRepository certificationRepository;
    private readonly UserManager<AppUser> userManager;
    private readonly IBlobStorageService blobStorageService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateApplicationCommandHandler(
        IApplicationRepository applicationRepository,
        ICertificationRepository certificationRepository,
        UserManager<AppUser> userManager,
        IBlobStorageService blobStorageService,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        this.applicationRepository = applicationRepository;
        this.certificationRepository = certificationRepository;
        this.userManager = userManager;
        this.blobStorageService = blobStorageService;
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.ApplicationResponse>> Handle(Command.CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        string? imageUrl = null;

        try
        {
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null || user.IsDeleted)
                return Result.Failure<Responses.ApplicationResponse>("Không tìm thấy người dùng.", StatusCodes.Status404NotFound);

            if (request.Image != null)
            {
                var firstName = request.Name.Split(' ').FirstOrDefault() ?? request.Name;
                var nameImage = $"{firstName}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

                imageUrl = await blobStorageService.UploadBlob(image: request.Image, name: nameImage, folder: BlobFolder.avatars)
                    ?? throw new Exception("Không thể tải tệp lên.");
            }

            var existingApplication = await applicationRepository.FindSingleAsync(x => x.UserId == request.UserId, cancellationToken);
            if (existingApplication != null)
            {
                if (existingApplication.Status == JobApplicationStatus.Cancel)
                {
                    applicationRepository.Remove(existingApplication);
                    await unitOfWork.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    return Result.Failure<Responses.ApplicationResponse>("Đơn ứng tuyển cho người dùng này đã tồn tại và chưa bị từ chối.", 400);
                }
            }

            var application = new Domain.Entities.Application();
            application.Create(request, imageUrl);

            await applicationRepository.AddAsync(application);
            await unitOfWork.SaveChangesAsync();

            List<string> certifications = new List<string>();
            if (request.Certifications != null && request.Certifications.Count > 0)
            {
                certifications = await AddCertifications(application.Id, request.Certifications);
            }

            var response = mapper.Map<Responses.ApplicationResponse>(application);
            response.ApplicationPetServices = application.ApplicationPetServices
                .Select(aps => new ApplicationPetServiceResponse
                {
                    PetServiceId = aps.PetServiceId
                }).ToList();

            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await blobStorageService.DeleteBlobSnapshotsAsync(imageUrl);
            }
            throw new Exception(ex.Message);
        }
    }

    private async Task<List<string>> AddCertifications(int applicationId, ICollection<IFormFile> images)
    {
        List<string> listImages = new List<string>();
        Random random = new Random();

        foreach (var image in images)
        {
            string fileName = $"{applicationId}-{DateTimeOffset.Now.ToUnixTimeMilliseconds() + random.Next(0, int.MaxValue)}";
            string urlString = await blobStorageService.UploadBlob(image: image, name: fileName, folder: BlobFolder.certifications);

            var certification = new Domain.Entities.Certification
            {
                ApplicationId = applicationId,
                Image = urlString
            };

            await certificationRepository.AddAsync(certification);
            listImages.Add(urlString);
        }
        return listImages;
    }
}
