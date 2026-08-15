using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class ShoeMeasurementsRepository : BaseRepository<ShoeMeasurement>, IShoeMeasurementsRepository
    {
        public ShoeMeasurementsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
