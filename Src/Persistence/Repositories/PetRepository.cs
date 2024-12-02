using Contract.Constants;
using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class PetRepository : AuditRepositoryBase<Pet, int>, IPetRepository
{
    private readonly ApplicationDbContext _context;

    public PetRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pet>> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Pet
              .Include(p => p.Breed)
              .Where(p => p.UserId == userId)
              .ToListAsync(cancellationToken);
    }

    public void SoftDelete(Pet pet)
    {
        pet.DeletedDate = TimeZones.GetSoutheastAsiaTime();
        pet.IsDeleted = true;

        Update(pet);
    }
}
