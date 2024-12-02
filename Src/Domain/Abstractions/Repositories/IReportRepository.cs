using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IReportRepository : IRepositoryBase<Report, int>
{
    Task<Report?> FindByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
}
