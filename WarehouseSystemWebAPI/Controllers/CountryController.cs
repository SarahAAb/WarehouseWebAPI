using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystemWebAPI.Models;
using WarehouseSystemWebAPI.Services;

namespace WarehouseSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
       ICountryService countryService;

        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }
        [Authorize(Roles = "Admin")]
        [Route("NewCountry")]
        [HttpPost]
        public void Insert (CountryDTO countryDTO)
        {
           countryService.Insert(countryDTO);
        }
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpGet]
        public void Delete (int id) {
        countryService.delete(id);
        }
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        [HttpPost]

        public void update(CountryDTO countryDTO)
        {
        countryService.Update(countryDTO);    
        }
        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpGet]
        public CountryDTO Edit(int id)
        {
            CountryDTO countryDTO = countryService.load(id);
            return countryDTO;

        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("CountryList")]
        [HttpGet]

        public List<CountryDTO> GetAll()
        {
            List<CountryDTO> countryDTOs=countryService.loadall();
            return countryDTOs;
        }
    }
}
