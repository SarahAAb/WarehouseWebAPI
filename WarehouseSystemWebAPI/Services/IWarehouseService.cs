using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public interface IWarehouseService
    {
        void insert(WarehouseDTO warehouse);
        void Update(WarehouseDTO warehouse);
        List<WarehouseDTO> loadall();
        void Delete(int Id);
         WarehouseDTO Edit(int Id);
        bool CheckName(string Name);
    }
}
