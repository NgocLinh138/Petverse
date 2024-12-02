using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;
using static Contract.Services.V1.PetCenter.Responses;

namespace Persistence.Repositories;

public class PetCenterServiceRepository : RepositoryBase<PetCenterService, int>, IPetCenterServiceRepository
{
    private readonly ApplicationDbContext context;

    public PetCenterServiceRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<PetCenterService?> FindByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken)
    {
        var serviceAppointment = await context.Set<ServiceAppointment>()
            .Include(x => x.PetCenterService)
            .FirstOrDefaultAsync(x => x.Id == appointmentId, cancellationToken);

        return serviceAppointment?.PetCenterService;
    }

    public async Task<IEnumerable<PetCenterServiceResponse>> GetPetCenterServiceResponsesByCenterIdAsync(Guid petCenterId)
    {
        var petCenterServices = await context.PetCenterService
            .Where(x => x.PetCenterId == petCenterId)
            .AsSplitQuery()
            .Include(x => x.PetService)
            .Include(x => x.ServiceAppointments).ThenInclude(x => x.AppointmentRate)
            .Select(pcs => new PetCenterServiceResponse(
                pcs.Id,
                pcs.PetService.Name,
                pcs.Price,
                pcs.Description,
                pcs.Type,
                pcs.Rate ?? 0,
                pcs.ServiceAppointments
                        .Select(sa => sa.AppointmentRate)
                        .Count(pcr => pcr != null)))
            .ToListAsync();

        return petCenterServices;
    }
}
