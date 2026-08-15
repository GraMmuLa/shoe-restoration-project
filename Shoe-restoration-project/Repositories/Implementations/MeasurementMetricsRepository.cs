using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class MeasurementMetricsRepository : BaseRepository<MeasurementMetric>, IMeasurementMetricsRepository
    {
        public MeasurementMetricsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
