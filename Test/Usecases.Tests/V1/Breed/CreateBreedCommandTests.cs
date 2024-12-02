using Application.Usecases.V1.Breed.Commands;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Usecases.Tests.V1.Breed;

public sealed class CreateBreedCommandTests
{
    private readonly IBreedRepository BreedRepository;
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly CreateBreedCommandHandler handler;
    private readonly Command.CreateBreedCommand request;
    private readonly Domain.Entities.Species Species;
    public CreateBreedCommandTests()
    {
        BreedRepository = A.Fake<IBreedRepository>();
        SpeciesRepository = A.Fake<ISpeciesRepository>();
        unitOfWork = A.Fake<IUnitOfWork>();
        mapper = A.Fake<IMapper>();
        handler = new CreateBreedCommandHandler(BreedRepository, mapper, unitOfWork, SpeciesRepository);

        Species = new Domain.Entities.Species { Id = 1, Name = "Dog" };

        request = new Command.CreateBreedCommand(
            Species.Id,
            "Small Dog",
            "Description of small dog");
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_SpeciesNotFound()
    {

        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.SpeciesId.Value, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Species?>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_BreedCreatedSuccessfully()
    {
        // Arrange
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.SpeciesId.Value, CancellationToken.None))
            .Returns(Task.FromResult(Species));
        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>._, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Breed>(null));
        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        var newBreed = new Domain.Entities.Breed
        {
            SpeciesId = request.SpeciesId.Value,
            Name = request.Name,
            Description = request.Description
        };

        A.CallTo(() => BreedRepository.AddAsync(A<Domain.Entities.Breed>.Ignored))
            .Invokes((Domain.Entities.Breed p) => newBreed = p)
            .Returns(Task.CompletedTask);

        var response = new Responses.BreedResponse
        {
            Id = 100,
            SpeciesId = request.SpeciesId.Value,
            Name = request.Name,
            Description = request.Description
        };

        A.CallTo(() => mapper.Map<Responses.BreedResponse>(A<Domain.Entities.Breed>.Ignored))
            .Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(201);
        result.Data.Should().BeEquivalentTo(response);
    }

    [Fact]
    public async Task Handle_Should_TriggerUnitOfWorkSaveChanges()
    {
        // Arrange
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.SpeciesId.Value, CancellationToken.None))
            .Returns(Task.FromResult(Species));
        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>._, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Breed>(null));
        // Act
        await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}
