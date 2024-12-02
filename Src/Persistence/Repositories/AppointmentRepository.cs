using Contract.Enumerations;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class AppointmentRepository : RepositoryBase<Appointment, Guid>, IAppointmentRepository
{
    private readonly ApplicationDbContext context;
    public AppointmentRepository(ApplicationDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<Appointment?> GetAppointmentById(Guid AppointmentId, AppointmentType? type)
    {
        IQueryable<Appointment> query = type switch
        {
            AppointmentType.BreedAppointment => context.Appointment.OfType<BreedAppointment>(),
            AppointmentType.ServiceAppointment => context.Appointment.OfType<ServiceAppointment>().Include(x => x.PetCenterService),
            _ => context.Appointment
        };

        var appointment = await query
            .Include(x => x.Pet).ThenInclude(x => x.Breed)
            .Include(x => x.Report)
            .Include(x => x.AppointmentRate)
            .Include(x => x.User)
            .Include(x => x.PetCenter).ThenInclude(x => x.Application)
            .FirstOrDefaultAsync(x => x.Id == AppointmentId);


        return appointment;
    }
}
