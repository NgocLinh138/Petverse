using Application.Usecases.V1.Species.Queries;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Usecases.Tests.V1.Species;

public sealed class GetSpeciesByIdQueryTests
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IMapper mapper;
    private readonly GetSpeciesByIdQueryHandler handler;

    public GetSpeciesByIdQueryTests()
    {
        this.SpeciesRepository = A.Fake<ISpeciesRepository>();
        this.mapper = A.Fake<IMapper>();
        this.handler = new GetSpeciesByIdQueryHandler(SpeciesRepository, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_SpeciesNotFound()
    {
        // Arrange
        var request = new Query.GetSpeciesByIdQuery { Id = 1 };

        // Giả lập không tìm thấy
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
    public async Task Handle_Should_ReturnSuccess_When_SpeciesFound()
    {
        // Arrange
        var Species = new Domain.Entities.Species { Id = 1, Name = "Dog" };
        var request = new Query.GetSpeciesByIdQuery { Id = Species.Id };

        // Giả lập tìm thấy
        A.CallTo(() => SpeciesRepository.FindByIdAsync(request.Id, CancellationToken.None))
            .Returns(Task.FromResult(Species));

        // Prepare response
        var response = new Responses.SpeciesResponse
        {
            Id = Species.Id,
            Name = Species.Name
        };

        // Giả lập mapper
        A.CallTo(() => mapper.Map<Responses.SpeciesResponse>(Species))
            .Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeEquivalentTo(response); // Kiểm tra dữ liệu trả về
    }
}
