using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IAppointmentRateRepository : IAuditRepositoryBase<AppointmentRate, int>
    {
        void SoftDelete(AppointmentRate petCenterRate);
        Task<int> CountServiceRate(int serviceRateId);
    }
}
