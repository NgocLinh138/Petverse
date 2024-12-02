using Domain.Abstractions.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Base;

namespace Persistence.Repositories;

public class ApplicationRepository : RepositoryBase<Application, int>, IApplicationRepository
{
    private readonly ApplicationDbContext _context;

    public ApplicationRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Application?> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Application
            .FirstOrDefaultAsync(app => app.UserId == userId, cancellationToken);
    }
}
