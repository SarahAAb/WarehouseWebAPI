using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarehouseSystemWebAPI.Models;
using WarehouseSystemWebAPI.Services;

namespace WarehouseSystemWebAPI.Controllers
{
    [Authorize (Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {


        ICityServices cityServices;

        ICountryService CountryServices;

        public CityController(ICityServices _cityServices, ICountryService countryServices)
        {
            cityServices = _cityServices;
            CountryServices = countryServices;
        }

        [Authorize(Roles = "Admin")]
        [Route("NewCity")]
        [HttpPost]
        public void NewCity(CityDTO cityDTO)
        {
            cityServices.Insert(cityDTO);
        }
        [Authorize(Roles = "Admin,Manager")]
        [Route("CityList")]
        [HttpGet]
        public List<CityDTO> CityList()
        {
          return cityServices.loadall();

        }
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        [HttpGet]
        public void Deleted(int id)
        {
            cityServices.Delete(id);
           
        }
        [Authorize(Roles = "Admin")]
        [Route("Edit")]
        [HttpGet]
        public CityDTO Edited(int id)
        {
            return cityServices.Edited(id);
        }
        [Authorize(Roles = "Admin")]
        [Route("Update")]
        [HttpPost]
        public void Update(CityDTO cityDTO)
        {
            cityServices.Update(cityDTO);
        }
    }


}
