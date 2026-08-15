using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class SizeMetricsRepository : BaseRepository<SizeMetric>, ISizeMetricsRepository
    {
        public SizeMetricsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
