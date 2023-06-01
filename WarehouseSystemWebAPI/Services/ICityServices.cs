using WarehouseSystemWebAPI.data;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public interface ICityServices
    {
        void Insert(CityDTO cityDTO);
        void Update(CityDTO cityDTO);
        void Delete(int id);
        List<CityDTO> loadall();
        CityDTO Edited(int Id);
        public List<CityDTO> load(int countryid);
    }
}
