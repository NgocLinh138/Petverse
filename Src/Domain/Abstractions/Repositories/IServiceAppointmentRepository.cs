using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IServiceAppointmentRepository : IRepositoryBase<ServiceAppointment, Guid>
{
}
