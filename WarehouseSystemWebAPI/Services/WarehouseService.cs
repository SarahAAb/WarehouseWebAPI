using WarehouseSystemWebAPI.data;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public class WarehouseService:IWarehouseService
    {
        WarehouseContext context;
        //IItemServices itemServices;

        public WarehouseService(WarehouseContext _context) //IItemServices _itemServices)
        {
            context = _context;
           // itemServices = _itemServices;
        }
        public void insert(WarehouseDTO warehouse)
        {
            Warehouse warehouse1 = new Warehouse()
            {

                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy = warehouse.CreatedBy,
                CreatedDAT = (DateTime)warehouse.CreatedDAT,
            };
            context.warehouses.Add(warehouse1);
            context.SaveChanges();
     
        }
        public void Update(WarehouseDTO warehouse)
        {
            Warehouse warehouse1 = new Warehouse()
            {
                Id = (int)warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy = (string)warehouse.CreatedBy,
                CreatedDAT = (DateTime)warehouse.CreatedDAT,
            };
            context.warehouses.Attach(warehouse1);
            context.Entry(warehouse1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public List<WarehouseDTO> loadall()
        {
            List<Warehouse> warehouses = context.warehouses.ToList();
            List<WarehouseDTO> warehouseDTOs = new List<WarehouseDTO>();
            foreach (Warehouse item in warehouses)
            {
                WarehouseDTO ll = new WarehouseDTO();
                ll.Id = item.Id;
                ll.Name = item.Name;
                ll.Description = item.Description;
                ll.CountryId = item.CountryId;
                ll.CityId = item.CityId;
                ll.CreatedBy = (string)item.CreatedBy;
                ll.CreatedDAT = (DateTime)item.CreatedDAT;
                warehouseDTOs.Add(ll);
            }
            return warehouseDTOs;
        }
        public WarehouseDTO Edit(int Id)
        {
            Warehouse warehouse = context.warehouses.Find(Id);
            WarehouseDTO warehouseDTO = new WarehouseDTO()
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy = (string)warehouse.CreatedBy,
                CreatedDAT =(DateTime) warehouse.CreatedDAT,

            };

            return warehouseDTO;
        }
        public void Delete(int Id)
        {
            Warehouse warehouse = context.warehouses.Find(Id);
            context.warehouses.Remove(warehouse);
            context.SaveChanges();

        }
        //public List<ItemDTO> view(int Id)
        //{
        //    List<Item> items = context.Items.Where(e => e.Warehouse_Id == Id).ToList();
        //    List<ItemDTO> itemDTOs = new List<ItemDTO>();
        //    foreach (Item item in items)
        //    {
        //        ItemDTO itemDTO = new ItemDTO()
        //        {
        //            Id = item.Id,
        //            Name = item.Name,
        //            MSRPPrice = item.MSRPPrice,
        //            Warehouse_Id = item.Warehouse_Id,
        //            CostPrice = item.CostPrice,
        //            QTY = item.QTY,
        //            CreatedBy = item.CreatedBy,
        //            IsDeleted = item.IsDeleted,
        //            KUCode = item.KUCode,
        //        };
        //        itemDTOs.Add(itemDTO);
        //    }
        //    return itemDTOs;
        //}
        public bool CheckName(string Name)
        {
            List<Warehouse> warehouse = context.warehouses.Where(e => e.Name == Name).ToList();
            if (warehouse.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
