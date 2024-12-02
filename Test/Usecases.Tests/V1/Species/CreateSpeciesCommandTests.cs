using Application.Usecases.V1.Species.Commands;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;

namespace Usecases.Tests.V1.Species;

public sealed class CreateSpeciesCommandTests
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly CreateSpeciesCommandHandler handler;
    private readonly Command.CreateSpeciesCommand request;
    public CreateSpeciesCommandTests()
    {
        this.SpeciesRepository = A.Fake<ISpeciesRepository>();
        this.unitOfWork = A.Fake<IUnitOfWork>();
        this.mapper = A.Fake<IMapper>();
        this.handler = new CreateSpeciesCommandHandler(SpeciesRepository, mapper, unitOfWork);

        request = new Command.CreateSpeciesCommand("Dog");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_SpeciesCreatedSuccessfully()
    {
        // Arrange
        var newSpecies = new Domain.Entities.Species
        {
            Name = request.Name
        };

        A.CallTo(() => SpeciesRepository.AddAsync(A<Domain.Entities.Species>.Ignored))
            .Invokes((Domain.Entities.Species p) => newSpecies = p)
            .Returns(Task.CompletedTask);

        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
            .Returns(Task.CompletedTask);

        var response = new Responses.SpeciesResponse
        {
            Id = 100,
            Name = request.Name
        };

        A.CallTo(() => mapper.Map<Responses.SpeciesResponse>(A<Domain.Entities.Species>.Ignored))
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
        var newSpecies = new Domain.Entities.Species
        {
            Name = request.Name
        };

        A.CallTo(() => SpeciesRepository.AddAsync(A<Domain.Entities.Species>.Ignored))
            .Returns(Task.CompletedTask);

        // Act
        await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => unitOfWork.SaveChangesAsync(A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }
}