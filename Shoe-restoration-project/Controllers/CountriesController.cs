using Microsoft.AspNetCore.Mvc;
using Shoe_restoration_project.DTO;
using Shoe_restoration_project.Models;
using Shoe_restoration_project.Services;

namespace Shoe_restoration_project.Controllers
{
    public class CountriesController : BaseApiController<CountryDto, Country>
    {
        private readonly ICountriesService _countryService;
        public CountriesController(ICountriesService countriesService) : base(countriesService)
        {
            _countryService = countriesService;
        }
    }
}
