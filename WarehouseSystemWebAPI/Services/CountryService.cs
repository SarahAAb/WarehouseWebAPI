using Microsoft.EntityFrameworkCore.Migrations.Operations;
using WarehouseSystemWebAPI.data;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public class CountryService:ICountryService
    {
        WarehouseContext context;

        public CountryService(WarehouseContext _context)
        {
            context = _context;
        }
        public void Insert(CountryDTO countryDTO)
        {
            Country country = new Country()
            {
                Name= countryDTO.Name,
                
            };
            context.countries.Add(country);
            context.SaveChanges();
        }
        public void Update(CountryDTO countryDTO)
        {
            Country country = new Country()
            {
                Id= (int)countryDTO.Id,
                Name = countryDTO.Name,

            };
            context.countries.Attach(country);
            context.Entry(country).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public void delete(int id)
        {
            Country country = context.countries.Find(id);
            context.countries.Remove(country);
            context.SaveChanges();
        }
        public CountryDTO load(int id)
        {
            Country country = context.countries.Find(id);
            CountryDTO countryDTO = new CountryDTO()
            {
                Id = country.Id,
                Name = country.Name
            };
            return countryDTO;
            
        }
        public List<CountryDTO> loadall()
        {
            List<Country> countries=context.countries.ToList();
            List<CountryDTO> countryDTOs=new List<CountryDTO>();
            if (countries.Count > 0)
            {
                foreach (var item in countries)
                {
                    countryDTOs.Add(new CountryDTO() { Id = item.Id, Name = item.Name });
                }
            }
            return countryDTOs;
        }
    }
}
