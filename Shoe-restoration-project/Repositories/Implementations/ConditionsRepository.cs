using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;

namespace Shoe_restoration_project.Repositories.Implementations
{
    public class ConditionsRepository : BaseRepository<Condition>, IConditionsRepository
    {
        public ConditionsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
