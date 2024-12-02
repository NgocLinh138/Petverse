using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class ReportRepository : RepositoryBase<Report, int>, IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Report?> FindByAppointmentIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Report
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.AppointmentId == appointmentId, cancellationToken);
        }
    }
}
