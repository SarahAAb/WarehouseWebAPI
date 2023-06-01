using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public interface ICountryService
    {
        void Insert(CountryDTO countryDTO);
        void Update(CountryDTO countryDTO);
        void delete(int id);
        CountryDTO load(int id);
        List<CountryDTO> loadall();
    }
}
