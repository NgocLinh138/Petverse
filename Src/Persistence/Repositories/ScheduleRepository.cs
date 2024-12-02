using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class ScheduleRepository : RepositoryBase<Schedule, int>, IScheduleRepository
{
    public ScheduleRepository(ApplicationDbContext context) : base(context)
    {
    }
}
