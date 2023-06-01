using Microsoft.EntityFrameworkCore;
using WarehouseSystemWebAPI.data;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.Services
{
    public class CityServices:ICityServices
    {
        WarehouseContext context;

        public CityServices(WarehouseContext _context)
        {
            context = _context;
        }
        public void Insert(CityDTO cityDTO){
            City city = new City()
            {
                Name= cityDTO.Name,
                CountryId= cityDTO.CountryId,
                
            };
            context.cities.Add(city);
            context.SaveChanges();
        }
        public void Update(CityDTO cityDTO)
        {
            City city = new City()
            {
                Id= (int)cityDTO.Id,
                Name = cityDTO.Name,
                CountryId = cityDTO.CountryId,

            };
            context.cities.Attach(city);
            context.Entry(city).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            City city = context.cities.Find(id);
            context.cities.Remove(city);
            context.SaveChanges();
        }
        public List<CityDTO> loadall()
        {
            List<City> citylist = context.cities.Include("Country").ToList();
            List<CityDTO> cityDTOs = new List<CityDTO>();
            foreach (City city in citylist)
            {
                CityDTO cityDTO = new CityDTO();
                cityDTO.Id = city.Id;
                cityDTO.Name = city.Name;
                cityDTO.CountryId = city.CountryId;
                cityDTO.countryDTO = new CountryDTO()
                {
                    Id = city.Country.Id,
                    Name = city.Country.Name,
                };

                cityDTOs.Add(cityDTO);

            }
            return cityDTOs;
        }
        public CityDTO Edited(int Id)
        {
            City city = context.cities.Find(Id);
            CityDTO cityDTO = new CityDTO();
            cityDTO.Id = city.Id;
            cityDTO.Name = city.Name;
            cityDTO.CountryId = city.CountryId;
            return cityDTO;
        }
        public List<CityDTO> load(int countryid)
        {
            List<City> citylist = context.cities.Where(e => e.CountryId == countryid).ToList();
            List<CityDTO> cityDTOs = new List<CityDTO>();
            foreach (City city in citylist)
            {
                CityDTO cityDTO = new CityDTO();
                cityDTO.Id = city.Id;
                cityDTO.Name = city.Name;
                cityDTO.CountryId = city.CountryId;
                

                cityDTOs.Add(cityDTO);

            }
            return cityDTOs;

        }

    }
}
