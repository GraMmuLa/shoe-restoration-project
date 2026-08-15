using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class ShoeTypesRepository : BaseRepository<ShoeType>, IShoeTypesRepository
    {
        public ShoeTypesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
