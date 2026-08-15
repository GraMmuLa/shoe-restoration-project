using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class ShoesRepository : BaseRepository<Shoe>, IShoesRepository
    {
        public ShoesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
