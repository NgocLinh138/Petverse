using Application.Usecases.V1.Job.Commands;
using AutoMapper;
using Contract.Enumerations;
using Contract.Services.V1.Job;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Job;

public sealed class CreateJobCommandTests
{
    private readonly IJobRepository jobRepository;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly ISpeciesJobRepository speciesJobRepository;
    private readonly IPetCenterServiceRepository petCenterServiceRepository;
    private readonly IMapper mapper;
    private readonly CreateJobCommandHandler commandHandler;
    private readonly Command.CreateJobCommand request;

    public CreateJobCommandTests()
    {
        jobRepository = A.Fake<IJobRepository>();
        petCenterRepository = A.Fake<IPetCenterRepository>();
        unitOfWork = A.Fake<IUnitOfWork>();
        speciesJobRepository = A.Fake<ISpeciesJobRepository>();
        petCenterServiceRepository = A.Fake<IPetCenterServiceRepository>();
        mapper = A.Fake<IMapper>();
        commandHandler = new CreateJobCommandHandler(
            jobRepository,
            mapper,
            petCenterRepository,
            unitOfWork,
            speciesJobRepository,
            petCenterServiceRepository
        );

        request = new Command.CreateJobCommand(
             Guid.NewGuid(),
             "descrip",
             true,
             true,
             true,
             new List<int> { 1, 2, 3 }, // Example species IDs
             new List<Command.PetCenterServiceUpdatePrice>
            {
                new Command.PetCenterServiceUpdatePrice ( 1, 100,  ServiceType.Fixed )
            }
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_PetCenterNotFound()
    {
        // Arrange
        A.CallTo(() => petCenterRepository.FindByIdAsync(request.PetCenterId, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.PetCenter>(null));

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Không tìm thấy trung tâm.");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_JobAlreadyExists()
    {
        // Arrange
        var petCenter = new Domain.Entities.PetCenter
        {
            Id = request.PetCenterId,
            Job = new Domain.Entities.Job() // Simulating that the job already exists
        };
        A.CallTo(() => petCenterRepository.FindByIdAsync(request.PetCenterId, CancellationToken.None))
            .Returns(Task.FromResult(petCenter));

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        result.Message.Should().Be("Công việc đã tồn tại.");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_JobIsCreated()
    {
        // Arrange
        var petCenter = new Domain.Entities.PetCenter { Id = request.PetCenterId };
        A.CallTo(() => petCenterRepository.FindByIdAsync(request.PetCenterId, CancellationToken.None))
            .Returns(Task.FromResult(petCenter));
        var newJob = new Domain.Entities.Job { Id = Guid.NewGuid() };
        A.CallTo(() => jobRepository.AddAsync(A<Domain.Entities.Job>.Ignored))
            .Invokes((Domain.Entities.Job job) => newJob = job);

        A.CallTo(() => mapper.Map<Responses.JobResponse>(newJob))
            .Returns(new Responses.JobResponse());
        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
    .Returns(Task.CompletedTask);
        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(201);
        A.CallTo(() => jobRepository.AddAsync(A<Domain.Entities.Job>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => speciesJobRepository.AddMultiAsync(A<IEnumerable<Domain.Entities.JunctionEntity.SpeciesJob>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
