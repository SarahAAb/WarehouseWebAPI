using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseSystemWebAPI.Models;

namespace WarehouseSystemWebAPI.data
{
    public class WarehouseContext:IdentityDbContext<ApplicationUsers>
    {
        IConfiguration configuration;

        public WarehouseContext(IConfiguration _configuration) {
            configuration = _configuration;
        }
        public DbSet<Country> countries { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Warehouse> warehouses { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("warehouseAPI"));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
