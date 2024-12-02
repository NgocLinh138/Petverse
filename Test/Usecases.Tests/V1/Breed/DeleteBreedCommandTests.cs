using Application.Usecases.V1.Breed.Commands;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
namespace Usecases.Tests.V1.Breed;

public sealed class DeleteBreedCommandTests
{
    private readonly IBreedRepository BreedRepository;
    private readonly DeleteBreedCommandHandler handler;
    private readonly Command.DeleteBreedCommand request;
    public DeleteBreedCommandTests()
    {
        BreedRepository = A.Fake<IBreedRepository>();
        handler = new DeleteBreedCommandHandler(BreedRepository);
        request = new Command.DeleteBreedCommand(1, 1);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_BreedNotFound()
    {
        // Arrange

        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>.Ignored, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Breed>(null));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Breed Not Found");
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_BreedDeletedSuccessfully()
    {
        // Arrange
        var Breed = new Domain.Entities.Breed { Id = 1, SpeciesId = 1, Name = "Small Dog" };

        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>.Ignored, CancellationToken.None))
            .Returns(Task.FromResult(Breed));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => BreedRepository.Remove(Breed)).MustHaveHappenedOnceExactly();
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
    }
}