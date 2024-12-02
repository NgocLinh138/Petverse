using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories;

public interface IPetRepository : IAuditRepositoryBase<Pet, int>
{
    Task<IEnumerable<Pet>> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    void SoftDelete(Pet pet);
}
