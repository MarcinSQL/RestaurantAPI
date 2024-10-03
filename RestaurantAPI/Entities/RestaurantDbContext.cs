using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=RestaurantDb;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //migration add => Tools>NuGet Package Manager>Package Manager Console>add-migration [name]
            //migration update => Package Manager Console>update-database
            modelBuilder.Entity<Restaurant>().Property(r => r.Name).HasMaxLength(25);

            modelBuilder.Entity<Address>().Property(a => a.City).HasMaxLength(50);
            modelBuilder.Entity<Address>().Property(a => a.Street).HasMaxLength(50);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
