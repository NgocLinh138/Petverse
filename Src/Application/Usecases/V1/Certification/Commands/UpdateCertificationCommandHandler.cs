using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.Certification;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.BlobStorage.Services;
using Infrastructure.BlobStorage.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Persistence.Repositories;

namespace Application.Usecases.V1.Certification.Commands
{
    public sealed class UpdateCertificationCommandHandler : ICommandHandler<Command.UpdateCertificationCommand, Responses.CertificationResponse>
    {
        private readonly ICertificationRepository certificationRepository;
        private readonly IBlobStorageService blobStorageService;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public UpdateCertificationCommandHandler(
            ICertificationRepository certificationRepository,
            IBlobStorageService blobStorageService,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.certificationRepository = certificationRepository;
            this.blobStorageService = blobStorageService;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Responses.CertificationResponse>> Handle(Command.UpdateCertificationCommand request, CancellationToken cancellationToken)
        {
            var certification = await certificationRepository.FindByIdAsync(request.Id, cancellationToken);
            if (certification == null)
                return Result.Failure<Responses.CertificationResponse>("Không tìm thấy đơn ứng tuyển.", StatusCodes.Status404NotFound);

            string? imageUrl = null;
            if (request.Image != null)
            {
                var nameImage = $"{certification.Application.Name}-{DateTimeOffset.Now.ToUnixTimeMilliseconds()}";

                imageUrl = await blobStorageService.UploadBlob(image: request.Image, name: nameImage, folder: BlobFolder.pets)
                            ?? throw new Exception("Không thể tải tệp lên.");

                certification.Image = imageUrl;
            }

            await unitOfWork.SaveChangesAsync();

            var response = mapper.Map<Responses.CertificationResponse>(certification);

            return Result.Success(response, 202);
        }
    }
}
