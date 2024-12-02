using Application.Usecases.V1.Species.Commands;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Species;

public sealed class DeleteSpeciesCommandTests
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly DeleteSpeciesCommandHandler handler;

    public DeleteSpeciesCommandTests()
    {
        this.SpeciesRepository = A.Fake<ISpeciesRepository>();
        this.handler = new DeleteSpeciesCommandHandler(SpeciesRepository);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_SpeciesNotFound()
    {
        // Arrange
        var request = new Command.DeleteSpeciesCommand(1);

        // Simulate not found
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Species>(null)); // Trả về null để mô phỏng không tìm thấy

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Species Not Found");
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_SpeciesDeletedSuccessfully()
    {
        // Arrange
        var Species = new Domain.Entities.Species { Id = 1, Name = "Dog" };
        var request = new Command.DeleteSpeciesCommand(Species.Id);

        // Simulate tìm thấy
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult(Species));

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => SpeciesRepository.Remove(Species)).MustHaveHappenedOnceExactly(); // Kiểm tra rằng Remove đã được gọi
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
    }
}