using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public interface IItemService
    {
        void Insert(ItemDTO item);
        void Update(ItemDTO item);
        List<ItemDTO> loadall();
        ItemDTO Edit(int Id);
        void SoftDelete(int Id);
        public bool CheckName(string Name);
        List<ItemDTO> view1(int Id);

    }
}
