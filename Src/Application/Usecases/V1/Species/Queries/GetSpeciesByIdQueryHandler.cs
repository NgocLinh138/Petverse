using AutoMapper;
using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.Species;
using Domain.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;


namespace Application.Usecases.V1.Species.Queries;
public sealed class GetSpeciesByIdQueryHandler : IQueryHandler<Query.GetSpeciesByIdQuery, Responses.SpeciesResponse>
{
    private readonly ISpeciesRepository SpeciesRepository;
    private readonly IMapper mapper;

    public GetSpeciesByIdQueryHandler(
         ISpeciesRepository SpeciesRepository,
        IMapper mapper)
    {
        this.SpeciesRepository = SpeciesRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.SpeciesResponse>> Handle(Query.GetSpeciesByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await SpeciesRepository.FindByIdAsync(request.Id);
        if (result == null)
            return Result.Failure<Responses.SpeciesResponse>("Không tìm thấy loại thú cưng.", StatusCodes.Status404NotFound);

        var resultResponse = mapper.Map<Responses.SpeciesResponse>(result);
        return Result.Success(resultResponse);
    }
}
