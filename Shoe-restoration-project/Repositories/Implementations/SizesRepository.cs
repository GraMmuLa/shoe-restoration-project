using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class SizesRepository : BaseRepository<Size>, ISizesRepository
    {
        public SizesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
