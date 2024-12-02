using Contract.Constants;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class AppointmentRateRepository : AuditRepositoryBase<AppointmentRate, int>, IAppointmentRateRepository
{
    private readonly ApplicationDbContext context;
    public AppointmentRateRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }
    public void SoftDelete(AppointmentRate AppointmentRate)
    {
        AppointmentRate.DeletedDate = TimeZones.GetSoutheastAsiaTime();
        AppointmentRate.IsDeleted = true;

        Update(AppointmentRate);
    }

    //public async Task<IList<AppointmentRate>> GetRatesByPetCenterServiceIdAsync(int petCenterServiceId)
    //{
    //    return await context.AppointmentRate
    //        .Include(r => r.Appointment)
    //        .Where(r => r.Appointment is ServiceAppointment &&
    //                    ((ServiceAppointment)r.Appointment).PetCenterServiceId == petCenterServiceId)
    //        .ToListAsync();
    //}

    public async Task<int> CountServiceRate(int serviceRateId)
    {
        var count = await context.AppointmentRate.Where(x =>
                    x.Id == serviceRateId
                    && x.Appointment.AppointmentRate != null).CountAsync();

        return count;
    }


}
