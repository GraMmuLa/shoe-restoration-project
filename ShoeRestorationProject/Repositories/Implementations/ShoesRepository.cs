using ShoeRestorationProject.Models;
using ShoeRestorationProject.Repositories.Implementations;

namespace ShoeRestorationProject.Repositories.Implementations
{
    public class ShoesRepository : Repository<Shoe, int>, IShoesRepository
    {
        public ShoesRepository(ShoeRestorationProject.Context.AppDbContext context) : base(context) { }
    }
}