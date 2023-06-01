using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystemWebAPI.Models;
using WarehouseSystemWebAPI.Services;

namespace WarehouseSystemWebAPI.Controllers
{
    [Authorize(Roles = "Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
     
            IWarehouseService warehouseServices;
            IItemService itemServices;

            IAccountService AccountServices;

            public ItemController(IWarehouseService _warehouseServices, IItemService _itemServices, IAccountService _accountServices)
            {
                warehouseServices = _warehouseServices;
                itemServices = _itemServices;
                AccountServices = _accountServices;
            }
        [Route("NewItem")]
        [HttpPost]
        public bool NewItem(ItemDTO itemDTO)
        {
            bool var = itemServices.CheckName(itemDTO.Name);
            if (var == true)
            {
                itemDTO.CreatedDAT = DateTime.Now;

                itemServices.Insert(itemDTO);
                return true;
            }
            else
            {
                return false;
            }
        }
        [Route("Update")]
        [HttpPost]
            public void Updated(ItemDTO  itemDTO)
            {
                itemServices.Update(itemDTO);
            }
        [Authorize(Roles = "Employee, Manager")]
        [Route("ItemList")]
        [HttpGet]
            public List<ItemDTO> ItemList()
            {
               return itemServices.loadall();
              
            }
        [Route("Edit")]
        [HttpGet]
        public ItemDTO Edited(int Id)
            {
                return itemServices.Edit(Id);
            }
        [Route("Delete")]
        [HttpGet]
        public void Deleted(int Id)
            {
                itemServices.SoftDelete(Id);             

            }
        }
    }


