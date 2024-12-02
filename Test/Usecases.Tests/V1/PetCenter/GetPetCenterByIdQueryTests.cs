using Application.Usecases.V1.PetCenter.Queries;
using AutoMapper;
using Contract.Enumerations;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using static Contract.Services.V1.Job.Responses;
using static Contract.Services.V1.PetCenter.Responses;

namespace Usecases.Tests.V1.PetCenter;

public sealed class GetPetCenterByIdQueryTests
{
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IApplicationRepository applicationRepository;
    private readonly IAppointmentRateRepository petCenterRateRepository;
    private readonly ISpeciesJobRepository speciesJobRepository;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    private readonly IMapper mapper;
    private readonly GetPetCenterByIdQueryHandler queryHandler;
    private readonly Query.GetPetCenterByIdQuery request;

    public GetPetCenterByIdQueryTests()
    {
        petCenterRepository = A.Fake<IPetCenterRepository>();
        applicationRepository = A.Fake<IApplicationRepository>();
        petCenterRateRepository = A.Fake<IAppointmentRateRepository>();
        speciesJobRepository = A.Fake<ISpeciesJobRepository>();
        petCenterServiceRepository = A.Fake<IPetCenterServiceRepository>();
        mapper = A.Fake<IMapper>();

        queryHandler = new GetPetCenterByIdQueryHandler(
            petCenterRepository,
            mapper,
            applicationRepository,
            petCenterRateRepository,
            speciesJobRepository,
            petCenterServiceRepository
        );

        request = new Query.GetPetCenterByIdQuery(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_PetCenterNotFound()
    {
        // Arrange
        A.CallTo(() => petCenterRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.PetCenter>(null));

        // Act
        var result = await queryHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Không tìm thấy trung tâm.");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_ApplicationNotFound()
    {
        // Arrange
        var petCenter = new Domain.Entities.PetCenter
        {
            Id = request.Id,
            ApplicationId = 1
        };
        A.CallTo(() => petCenterRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult(petCenter));
        A.CallTo(() => applicationRepository.FindByIdAsync(petCenter.ApplicationId, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Application>(null));

        // Act
        var result = await queryHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_PetCenterAndApplicationExist()
    {
        // Arrange
        var petCenter = new Domain.Entities.PetCenter
        {
            Id = request.Id,
            ApplicationId = 1,
            Application = new Domain.Entities.Application
            {
                Name = "applicationName"
            },
            Job = new Domain.Entities.Job
            {
                Id = Guid.NewGuid()
            }
        };
        var application = new Domain.Entities.Application { Id = 1, Name = "Pet Center Name" };
        IEnumerable<PetCenterServiceResponse> petCenterServiceResponses = new List<PetCenterServiceResponse>
        {
            new PetCenterServiceResponse(1, "Service Name", 100m, "Service Description", ServiceType.Fixed, 4.5f, 10)
        };

        A.CallTo(() => petCenterRepository.FindByIdAsync(request.Id, CancellationToken.None)).Returns(Task.FromResult(petCenter));
        A.CallTo(() => applicationRepository.FindByIdAsync(petCenter.ApplicationId, CancellationToken.None))
            .Returns(Task.FromResult(application));
        A.CallTo(() => petCenterServiceRepository.GetPetCenterServiceResponsesByCenterIdAsync(request.Id))
            .Returns(Task.FromResult(petCenterServiceResponses));

        var species = new List<string> { "Species1", "Species2" };
        A.CallTo(() => speciesJobRepository.GetSpeciesNameByJobId(petCenter.Job.Id)).Returns(species);

        var certifications = new List<CertificationResponse>
        {
            new CertificationResponse(1, "Certification Image")
        };
        A.CallTo(() => mapper.Map<ICollection<CertificationResponse>>(application.Certifications))
            .Returns(certifications);

        var jobResponse = new JobResponse(); // Giả định một JobResponse hợp lệ
        A.CallTo(() => mapper.Map<JobResponse>(petCenter.Job)).Returns(jobResponse);

        // Act
        var result = await queryHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(200);
    }
}
