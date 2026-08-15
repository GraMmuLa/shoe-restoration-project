using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class SkinTypesRepository : BaseRepository<SkinType>, ISkinTypesRepository
    {
        public SkinTypesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
