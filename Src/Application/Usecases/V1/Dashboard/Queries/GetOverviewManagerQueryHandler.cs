using Contract.Abstractions.Message;
using Contract.Abstractions.Shared;
using Contract.Constants;
using Contract.Services.V1.Dashboard;
using Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Application.Usecases.V1.Dashboard.Queries;

public sealed class GetOverviewManagerQueryHandler : IQueryHandler<Query.GetOverViewManagerQuery, Responses.OverviewManagerResponse>
{
    private readonly IPetRepository petRepository;
    private readonly IPetCenterRepository petCenterRepository;
    private readonly ICenterBreedRepository centerBreedRepository;
    private readonly DateTime ThisMonth;
    public GetOverviewManagerQueryHandler(IPetRepository petRepository, IPetCenterRepository petCenterRepository, ICenterBreedRepository centerBreedRepository)
    {
        this.petRepository = petRepository;
        this.petCenterRepository = petCenterRepository;
        this.centerBreedRepository = centerBreedRepository;
        ThisMonth = new DateTime(TimeZones.GetSoutheastAsiaTime().Year, TimeZones.GetSoutheastAsiaTime().Month, 1);
    }

    public async Task<Result<Responses.OverviewManagerResponse>> Handle(Query.GetOverViewManagerQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var totalPet = await petRepository.FindAll().CountAsync();
            var petcenters = petCenterRepository.FindAll(includeProperties: x => x.PetCenterServices);
            var totalPetCenter = await petcenters.CountAsync();
            var totalServices = petCenterRepository.FindAll(x => x.CreatedDate >= ThisMonth, includeProperties: x => x.PetCenterServices)
                                .Include(x => x.PetCenterServices).Select(x => x.PetCenterServices.Count()).ToList().Sum();

            var totalCenterBreed = await centerBreedRepository.FindAll(x => x.CreatedDate >= ThisMonth).CountAsync();

            var response = new Responses.OverviewManagerResponse(
                totalPet,
                totalPetCenter,
                totalServices,
                totalCenterBreed);

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}
