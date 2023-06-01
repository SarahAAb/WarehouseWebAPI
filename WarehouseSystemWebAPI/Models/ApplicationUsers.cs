using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseSystemWebAPI.data;

namespace WarehouseSystemWebAPI.Models
{
    public class ApplicationUsers:IdentityUser
    {
        public string FullName { get; set; }
        public bool Active { get; set; }
       public Warehouse Warehouse { get; set; }
        [ForeignKey("Warehouse")]
        public int Warehouse_Id { get; set; }
    }
}
