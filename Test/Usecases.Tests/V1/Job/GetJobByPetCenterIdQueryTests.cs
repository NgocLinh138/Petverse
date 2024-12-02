using Application.Usecases.V1.Job.Queries;
using AutoMapper;
using Contract.Services.V1.Job;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Usecases.Tests.V1.Job;

public sealed class GetJobByPetCenterIdQueryTests
{
    private readonly IJobRepository jobRepository;
    private readonly IMapper mapper;
    private readonly GetJobByPetCenterIdQueryHandler queryHandler;
    private readonly Query.GetJobByPetCenterIdQuery request;
    public GetJobByPetCenterIdQueryTests()
    {
        jobRepository = A.Fake<IJobRepository>();
        mapper = A.Fake<IMapper>();
        queryHandler = new GetJobByPetCenterIdQueryHandler(jobRepository, mapper);
        request = new Query.GetJobByPetCenterIdQuery { PetCenterId = Guid.NewGuid() };

    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_JobNotFound()
    {
        // Arrange
        A.CallTo(() => jobRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Job, bool>>>._, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Job>(null));

        // Act
        var result = await queryHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
        result.Message.Should().Be("Job Not Found");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_JobFound()
    {
        // Arrange
        var job = new Domain.Entities.Job { Id = Guid.NewGuid(), PetCenterId = Guid.NewGuid() };
        var jobResponse = new Responses.JobResponse();

        A.CallTo(() => jobRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Job, bool>>>._, CancellationToken.None))
            .Returns(job);
        A.CallTo(() => mapper.Map<Responses.JobResponse>(job)).Returns(jobResponse);

        // Act
        var result = await queryHandler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(jobResponse);
    }
}
