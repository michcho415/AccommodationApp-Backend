using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>();
            modelBuilder.Entity<ApartmentFeature>();
            modelBuilder.Entity<BedPrize>();
            modelBuilder.Entity<Landlord>();
            modelBuilder.Entity<Rental>();
            var admin = new User()
            {
                ID = 1,
                Username = "Admin",
                EmailAddress = "dupa@a.com",
                IsAdmin = true
            };
            PasswordHasher<User> hasher = new();
            admin.Password = hasher.HashPassword(admin, "admin");
            modelBuilder.Entity<User>().HasData(admin);
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<ApartmentFeature> ApartmentsFeatures { get; set; }

        public DbSet<BedPrize> BedPrizes { get; set; }

        public DbSet<Landlord> Landlords { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        public DbSet<User> Users { get; set; }

    }
}