using Application.Usecases.V1.Species.Commands;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Species;

public sealed class UpdateSpeciesCommandTests
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IMapper mapper;
    private readonly UpdateSpeciesCommandHandler handler;

    public UpdateSpeciesCommandTests()
    {
        this.SpeciesRepository = A.Fake<ISpeciesRepository>();
        this.mapper = A.Fake<IMapper>();
        this.handler = new UpdateSpeciesCommandHandler(SpeciesRepository, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_SpeciesNotFound()
    {
        // Arrange
        var request = new Command.UpdateSpeciesCommand(1, "Dog");

        // Simulate not found
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.Id.Value, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Species>(null)); // Trả về null để mô phỏng không tìm thấy

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_SpeciesUpdatedSuccessfully()
    {
        // Arrange
        var Species = new Domain.Entities.Species { Id = 1, Name = "Old Dog" };
        var request = new Command.UpdateSpeciesCommand(Species.Id, "Updated Dog");

        // Simulate tìm thấy
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.Id.Value, CancellationToken.None))
            .Returns(Task.FromResult(Species));

        // Prepare response
        var response = new Responses.SpeciesResponse
        {
            Id = Species.Id,
            Name = request.Name
        };

        // Simulate mapper
        A.CallTo(() => mapper.Map<Responses.SpeciesResponse>(Species))
            .Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => SpeciesRepository.Update(Species)).MustHaveHappenedOnceExactly(); // Kiểm tra rằng Update đã được gọi
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
        result.Data.Should().BeEquivalentTo(response); // Kiểm tra dữ liệu trả về
    }
}
