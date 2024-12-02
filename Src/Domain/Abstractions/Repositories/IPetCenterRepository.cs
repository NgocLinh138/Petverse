using Domain.Abstractions.Repositories.Base;
using Domain.Entities;
using static Contract.Services.V1.Dashboard.Responses;
using static Contract.Services.V1.PetCenter.Responses;

namespace Domain.Abstractions.Repositories;

public interface IPetCenterRepository : IRepositoryBase<PetCenter, Guid>
{
    void SoftDelete(PetCenter petSitter);
    Task<List<TopPetCenter>> GetTopPetCenterOfService();
    Task<List<TopPetCenterData>> GetTop5PetCentersAsync(int month, int year);
}
