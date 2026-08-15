using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class MeasurementPropertiesRepository : BaseRepository<MeasurementProperty>, IMeasurementPropertiesRepository
    {
        public MeasurementPropertiesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
