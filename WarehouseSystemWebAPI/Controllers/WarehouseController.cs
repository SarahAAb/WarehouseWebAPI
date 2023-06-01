using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystemWebAPI.Models;
using WarehouseSystemWebAPI.Services;

namespace WarehouseSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

        ICountryService countryServices;
        IItemService itemService;
        ICityServices cityServices;
        IWarehouseService warehouseServices;

            public WarehouseController(ICountryService _countryServices,IItemService _itemService, ICityServices _cityServices, IWarehouseService _warehouseServices)
            {
                countryServices = _countryServices;
            itemService = _itemService;
            cityServices = _cityServices;
                warehouseServices = _warehouseServices;
            }

        [Authorize(Roles = "Manager")]
        [Route("NewWarehouse")]
        [HttpPost]
            public bool NewWarehouse(WarehouseDTO warehouse)
            {
            bool result=warehouseServices.CheckName(warehouse.Name);
            if (result==true)
            {
                warehouse.CreatedDAT = DateTime.Now;
                warehouseServices.insert(warehouse);
                return true;
            }
            else
            {
                return false;
            }

            }
        [Authorize]
        [Route("WarehouseList")]
        [HttpGet]
        public List<WarehouseDTO> WarehouseList()
            {
                return warehouseServices.loadall();
            }
        [Authorize(Roles = "Manager")]
        [Route("Edit")]
        [HttpGet]
            public WarehouseDTO  Edited(int Id)
            {
               return warehouseServices.Edit(Id);

            }
        [Authorize(Roles = "Manager")]
        [Route("Update")]
        [HttpPost]
            public void Updated(WarehouseDTO warehouse)
            {
                warehouseServices.Update(warehouse);


            }
        [Authorize(Roles = "Manager")]
        [Route("Delete")]
        [HttpGet]
            public void Deleted(int Id)
            {
                warehouseServices.Delete(Id);
               

            }
        [Authorize(Roles = "Manager,Employee")]
        [Route("View")]
        [HttpGet]
        public List<ItemDTO> View(int id)
        {
            
          return  itemService.view1(id);  
            
        }
        [Authorize(Roles = "Manager,Admin")]
        [Route("Load")]
        [HttpGet]
            public List<CityDTO> Loadcity(int countryid)
        {
            List<CityDTO> cities =cityServices.load(countryid);
            return cities;
        }

        }
    }


