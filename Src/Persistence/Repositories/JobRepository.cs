using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class JobRepository : RepositoryBase<Job, Guid>, IJobRepository
{
    public JobRepository(ApplicationDbContext context) : base(context)
    {
    }
}
