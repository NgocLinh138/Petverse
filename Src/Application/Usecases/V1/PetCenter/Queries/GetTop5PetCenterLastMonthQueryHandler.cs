using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Services.V1.PetCenter;
using Domain.Abstractions.Repositories;

namespace Application.Usecases.V1.PetCenter.Queries;

public sealed class GetTop5PetCenterLastMonthQueryHandler : IQueryHandler<Query.GetTop5PetCenterQuery, Responses.TopPetCenterResponse>
{
    private readonly IPetCenterRepository petCenterRepository;

    public GetTop5PetCenterLastMonthQueryHandler(IPetCenterRepository petCenterRepository)
    {
        this.petCenterRepository = petCenterRepository;
    }

    public async Task<Result<Responses.TopPetCenterResponse>> Handle(Query.GetTop5PetCenterQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var data = await petCenterRepository.GetTop5PetCentersAsync(request.Month, request.Year);

            return Result.Success(new Responses.TopPetCenterResponse(data));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
}
