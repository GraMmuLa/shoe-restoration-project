using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class ColorsRepository : BaseRepository<Color>, IColorsRepository
    {
        public ColorsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
