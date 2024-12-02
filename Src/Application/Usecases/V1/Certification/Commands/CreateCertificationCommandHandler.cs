using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.Certification;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Application.Usecases.V1.Certification.Commands
{
    public sealed class CreateCertificationCommandHandler : ICommandHandler<Command.CreateCertificationCommand, Responses.CertificationResponse>
    {
        private readonly ICertificationRepository certificationRepository;
        private readonly IApplicationRepository applicationRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public CreateCertificationCommandHandler(
            ICertificationRepository certificationRepository,
            IApplicationRepository applicationRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.certificationRepository = certificationRepository;
            this.applicationRepository = applicationRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.CertificationResponse>> Handle(Command.CreateCertificationCommand request, CancellationToken cancellationToken)
        {
            var application = await applicationRepository.FindByIdAsync(request.ApplicationId, cancellationToken);
            if (application == null)
                return Result.Failure<Responses.CertificationResponse>("Không tìm thấy đơn ứng tuyển.", StatusCodes.Status404NotFound);

            string? imageUrl = null;

            try
            {
                if (request.Image != null)
                {
                    var nameImage = $"{application.Name}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

                    imageUrl = await blobStorageService.UploadBlob(image: request.Image, name: nameImage, folder: BlobFolder.certifications)
                        ?? throw new Exception("Không thể tải tệp lên.");
                }

                var certification = new Domain.Entities.Certification
                {
                    ApplicationId = request.ApplicationId,
                    Image = imageUrl
                };


                await certificationRepository.AddAsync(certification);
                await unitOfWork.SaveChangesAsync();

                var response = mapper.Map<Responses.CertificationResponse>(certification);
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
    }
}
