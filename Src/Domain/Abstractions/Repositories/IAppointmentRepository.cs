using Contract.Enumerations;
using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IAppointmentRepository : IRepositoryBase<Appointment, Guid>
{
    Task<Appointment?> GetAppointmentById(Guid AppointmentId, AppointmentType? type);
}
