using System.ComponentModel.DataAnnotations;

namespace WarehouseSystemWebAPI.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
    }
}
