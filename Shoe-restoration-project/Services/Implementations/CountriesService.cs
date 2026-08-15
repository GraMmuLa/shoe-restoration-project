using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Helpers;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Repositories;

namespace Shoe_restoration_project.Services.Implementations
{
    public class CountriesService : BaseService<CountryDto, Country>, ICountriesService
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository, IUnitOfWork unitOfWork) : base(countriesRepository, unitOfWork)
        {
            _countriesRepository = countriesRepository;
        }
    }
}
