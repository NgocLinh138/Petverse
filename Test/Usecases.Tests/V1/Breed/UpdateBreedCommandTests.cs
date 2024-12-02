using Application.Usecases.V1.Breed.Commands;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Usecases.Tests.V1.Breed;

public sealed class UpdateBreedCommandTests
{
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;
    private readonly UpdateBreedCommandHandler handler;

    public UpdateBreedCommandTests()
    {
        BreedRepository = A.Fake<IBreedRepository>();
        mapper = A.Fake<IMapper>();
        handler = new UpdateBreedCommandHandler(BreedRepository, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_BreedNotFound()
    {
        // Arrange
        var request = new Command.UpdateBreedCommand(1, 1, "Small Dog", "Description of small dog");

        // Simulate not found
        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>.Ignored, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Breed>(null)); // Trả về null để mô phỏng không tìm thấy

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Breed Not Found");
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_BreedUpdatedSuccessfully()
    {
        // Arrange
        var Breed = new Domain.Entities.Breed { Id = 1, SpeciesId = 1, Name = "Small Dog", Description = "Old Description" };
        var request = new Command.UpdateBreedCommand(Breed.Id, Breed.SpeciesId, "Updated Small Dog", "Updated Description");

        // Simulate tìm thấy
        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>.Ignored, CancellationToken.None))
            .Returns(Task.FromResult(Breed));

        // Prepare response
        var response = new Responses.BreedResponse
        {
            SpeciesId = Breed.SpeciesId,
            Name = request.Name,
            Description = request.Description
        };

        // Simulate mapper
        A.CallTo(() => mapper.Map<Responses.BreedResponse>(Breed))
            .Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        A.CallTo(() => BreedRepository.Update(Breed)).MustHaveHappenedOnceExactly(); // Kiểm tra rằng Update đã được gọi
        result.IsSuccess.Should().BeTrue();
        result.StatusCode.Should().Be(202);
        result.Data.Should().BeEquivalentTo(response); // Kiểm tra dữ liệu trả về
    }
}
