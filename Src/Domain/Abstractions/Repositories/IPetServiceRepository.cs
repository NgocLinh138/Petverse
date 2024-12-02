using Domain.Abstractions.EntityBase;
using Domain.Abstractions.Repositories.Base;
using Domain.Entities;

namespace Domain.Abstractions.Repositories
{
    public interface IPetServiceRepository : IRepositoryBase<PetService, int>
    {
    }
}
