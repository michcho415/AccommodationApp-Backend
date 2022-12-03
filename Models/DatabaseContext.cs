using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DatabaseContext(IConfiguration configuration) : base()
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>();
            modelBuilder.Entity<ApartmentFeature>();
            modelBuilder.Entity<BedPrize>();
            modelBuilder.Entity<Rental>();
            modelBuilder.Entity<User>();
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<ApartmentFeature> ApartmentsFeatures { get; set; }

        public DbSet<BedPrize> BedPrizes { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<User> Users { get; set; }

    }
}