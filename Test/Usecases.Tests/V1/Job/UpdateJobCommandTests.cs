using Application.Usecases.V1.Job.Commands;
using AutoMapper;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using Domain.Entities.JunctionEntity;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Job;

public sealed class UpdateJobCommandTests
{
    private readonly IJobRepository jobRepository;
    private readonly ISpeciesJobRepository speciesJobRepository;
    private readonly IMapper mapper;
    private readonly UpdateJobCommandHandler commandHandler;
    private readonly Command.UpdateJobCommand request;

    public UpdateJobCommandTests()
    {
        jobRepository = A.Fake<IJobRepository>();
        speciesJobRepository = A.Fake<ISpeciesJobRepository>();
        mapper = A.Fake<IMapper>();
        commandHandler = new UpdateJobCommandHandler(jobRepository, mapper, speciesJobRepository);
        request = new Command.UpdateJobCommand(
            Guid.NewGuid(),
            "description",
            true,
            true,
            true,
            new List<int> { 1, 2, 3 } // Example species IDs
        );
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_JobNotFound()
    {
        // Arrange
        A.CallTo(() => jobRepository.FindByIdAsync(request.Id.Value, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Job>(null));

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Không tìm thấy công việc.");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_JobFoundAndUpdated()
    {
        // Arrange
        var job = new Domain.Entities.Job { Id = request.Id.Value };
        A.CallTo(() => jobRepository.FindByIdAsync(request.Id.Value, CancellationToken.None))
            .Returns(Task.FromResult(job));
        A.CallTo(() => mapper.Map<Responses.JobResponse>(job))
            .Returns(new Responses.JobResponse());

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
        A.CallTo(() => jobRepository.Update(job)).MustHaveHappenedOnceExactly();
        A.CallTo(() => speciesJobRepository.RemoveMulti(A<ICollection<SpeciesJob>>.Ignored)).MustHaveHappenedOnceExactly();
        A.CallTo(() => speciesJobRepository.AddMultiAsync(A<ICollection<SpeciesJob>>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
