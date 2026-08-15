using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class BrandsRepository : BaseRepository<Brand>, IBrandsRepository
    {
        public BrandsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
