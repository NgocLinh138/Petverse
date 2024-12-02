using Domain.Abstractions.Repositories;
using Domain.Entities;
using Persistence.Repositories.Base;

namespace Persistence.Repositories
{
    public class ReportImageRepository : RepositoryBase<ReportImage, int>, IReportImageRepository
    {
        public ReportImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
