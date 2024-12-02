using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class ServiceAppointmentRepository : RepositoryBase<ServiceAppointment, Guid>, IServiceAppointmentRepository
{
    public ServiceAppointmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
