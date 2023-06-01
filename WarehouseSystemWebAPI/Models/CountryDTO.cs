using System.ComponentModel.DataAnnotations;

namespace WarehouseSystemWebAPI.Models
{
    public class CountryDTO
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
