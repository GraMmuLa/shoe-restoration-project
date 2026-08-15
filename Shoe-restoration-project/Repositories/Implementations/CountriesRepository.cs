using Microsoft.EntityFrameworkCore;
using Shoe_restoration_project.Context;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories.Implementations;

namespace Shoe_restoration_project.Repositories.Implementation
{
    public class CountriesRepository : BaseRepository<Country>, ICountriesRepository
    {
        public CountriesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
