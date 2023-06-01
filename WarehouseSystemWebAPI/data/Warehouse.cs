using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseSystemWebAPI.data;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.data
{
    [Table("Warehouses")]
    public class Warehouse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
       public string Description { get; set; }
        [Required]
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }
        public Country Country { get; set; }
        public City City { get; set; }
        public List<ApplicationUsers> ApplicationUsers { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDAT { get; set; }
        public List<Item> Items { get; set; }
    }
}
