using DTO.InputDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class LandlordService : ILandlordService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;
        private readonly PasswordHasher<Landlord> hasher = new();

        public LandlordService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IActionResult> CreateAsync(LandlordDTO landlord)
        {
            var existingLandlord = await GetAsync(landlord.Username);
            if (existingLandlord != null)
                return new UnprocessableEntityResult();
            var existingEmail = await GetByEmailAsync(landlord.EmailAddress);
            if (existingEmail != null)
                return new UnprocessableEntityResult();

            var newLandlord = new Landlord()
            {
                Username = landlord.Username,
                Phone = landlord.Phone,
                EmailAddress = landlord.EmailAddress
            };

            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var result = await context.Landlords.AddAsync(newLandlord);
                var hashedPassword = hasher.HashPassword(newLandlord, landlord.Password);
                newLandlord.Password = hashedPassword;

                await context.SaveChangesAsync();
            }
            return new OkResult();
        }

        private async Task<Landlord?> GetByEmailAsync(string emailAddress)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Landlords.FirstOrDefaultAsync(l => l.EmailAddress == emailAddress);
        }

        private async Task<Landlord?> GetAsync(string username)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Landlords.FirstOrDefaultAsync(l => l.Username == username);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var landlordToDelete = await context.Landlords.FindAsync(id);
                if (landlordToDelete != null)
                {
                    context.Landlords.Remove(landlordToDelete);
                    await context.SaveChangesAsync();
                }
            }
            return new OkResult();
        }

        public async Task<IActionResult> AuthenticateAsync(LoginDTO login)
        {
            var landlord = await GetAsync(login.Login);
            if (landlord == null || hasher.VerifyHashedPassword(landlord, landlord.Password, login.Password) == PasswordVerificationResult.Failed)
                return new UnprocessableEntityResult();
            return new OkResult();
        }
    }
}
