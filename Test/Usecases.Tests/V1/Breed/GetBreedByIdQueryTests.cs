using Application.Usecases.V1.Breed.Queries;
using AutoMapper;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Usecases.Tests.V1.Breed;

public sealed class GetBreedByIdQueryTests
{
    private readonly IBreedRepository BreedRepository;
    private readonly IMapper mapper;
    private readonly GetBreedByIdQueryHandler handler;

    public GetBreedByIdQueryTests()
    {
        BreedRepository = A.Fake<IBreedRepository>();
        mapper = A.Fake<IMapper>();
        handler = new GetBreedByIdQueryHandler(BreedRepository, mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_When_BreedNotFound()
    {
        // Arrange
        var request = new Query.GetBreedByIdQuery { Id = 1, SpeciesId = 1 };

        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>._, CancellationToken.None))
            .Returns(Task.FromResult<Domain.Entities.Breed>(null));
        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_When_BreedFound()
    {
        // Arrange
        var Breed = new Domain.Entities.Breed { Id = 1, Name = "Small Dog", SpeciesId = 1 };
        var request = new Query.GetBreedByIdQuery { Id = Breed.Id };

        A.CallTo(() => BreedRepository.FindSingleAsync(A<Expression<Func<Domain.Entities.Breed, bool>>>._, CancellationToken.None))
            .Returns(Task.FromResult(Breed));

        var response = new Responses.BreedResponse
        {
            Id = Breed.Id,
            Name = Breed.Name,
            SpeciesId = Breed.SpeciesId
        };

        A.CallTo(() => mapper.Map<Responses.BreedResponse>(Breed))
            .Returns(response);

        // Act
        var result = await handler.Handle(request, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeEquivalentTo(response);
    }
}
