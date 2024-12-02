using Domain.Abstractions.Repositories.Base;
using Domain.Entities;
using static Contract.Services.V1.PetCenter.Responses;

namespace Domain.Abstractions.Repositories;

public interface IPetCenterServiceRepository : IRepositoryBase<PetCenterService, int>
{
    Task<PetCenterService?> FindByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken);
    Task<IEnumerable<PetCenterServiceResponse>> GetPetCenterServiceResponsesByCenterIdAsync(Guid petCenterId);
}
