using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class MeasurementValuesRepository : BaseRepository<MeasurementValue>, IMeasurementValuesRepository
    {
        public MeasurementValuesRepository(AppDbContext context) : base(context)
        {
        }
    }
}
