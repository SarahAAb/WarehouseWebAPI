using System.ComponentModel.DataAnnotations.Schema;
using WarehouseSystemWebAPI.data;

namespace WarehouseSystemWebAPI.Models
{
    public class UsersDTO
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public bool Active { get; set; }
        public string Email { get; set; }
        public int Warehouse_Id { get; set; }
    }
}
