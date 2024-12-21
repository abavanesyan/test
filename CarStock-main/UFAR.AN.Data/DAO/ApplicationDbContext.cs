using Microsoft.EntityFrameworkCore;
using UFAR.AN.Data.Entities;

namespace UFAR.AN.Data.DAO
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); // Call base method for configuring DbContext options
        }

        // DbSet for the CarsEntity class to interact with the Cars table
        public DbSet<CarsEntity> Cars { get; set; }

        // DbSet for the ItemEntity class to interact with the Items table
        public DbSet<ItemEntity> Items { get; set; }
    }
}
