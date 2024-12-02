using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IApplicationRepository : IRepositoryBase<Application, int>
{
    Task<Application?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}

