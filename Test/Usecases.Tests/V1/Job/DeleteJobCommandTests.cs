using Application.Usecases.V1.Job.Commands;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Job;

public sealed class DeleteJobCommandTests
{
    private readonly IJobRepository jobRepository;
    private readonly DeleteJobCommandHandler commandHandler;
    private readonly Command.DeleteJobCommand request;
    public DeleteJobCommandTests()
    {
        jobRepository = A.Fake<IJobRepository>();
        commandHandler = new DeleteJobCommandHandler(jobRepository);
        request = new Command.DeleteJobCommand(Guid.NewGuid());
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_JobNotFound()
    {
        // Arrange
        A.CallTo(() => jobRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Job>(null));

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Không tìm thấy công việc.");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_JobFoundAndRemoved()
    {
        // Arrange
        var job = new Domain.Entities.Job { Id = Guid.NewGuid() };

        A.CallTo(() => jobRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult(job));

        // Act
        var result = await commandHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
        A.CallTo(() => jobRepository.Remove(job)).MustHaveHappenedOnceExactly();
    }
}
