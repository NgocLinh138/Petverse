using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IJobRepository : IRepositoryBase<Job, Guid>
{
}
