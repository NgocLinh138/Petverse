using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class BreedAppointmentRepository : RepositoryBase<BreedAppointment, Guid>, IBreedAppointmentRepository
{
    public BreedAppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
