using Common;
using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class LandlordService : ILandlordService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;
        private readonly PasswordHasher<Landlord> hasher = new();
        private readonly AppSettings appSettings;

        public LandlordService(IDbContextFactory<DatabaseContext> contextFactory, IOptions<AppSettings> appSettings)
        {
            this.contextFactory = contextFactory;
            this.appSettings = appSettings.Value;
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

        public async Task<Landlord?> GetAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Landlords.FindAsync(id);
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

        public async Task<LoginResponseDTO?> AuthenticateAsync(LoginDTO login)
        {
            var landlord = await GetAsync(login.Login);
            if (landlord == null || hasher.VerifyHashedPassword(landlord, landlord.Password, login.Password) == PasswordVerificationResult.Failed)
                return null;

            var token = generateJwtToken(landlord);
            return new LoginResponseDTO(landlord, token);
        }

        public async Task<ICollection<Landlord>?> GetAll()
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Landlords.ToListAsync();
        }

        private string generateJwtToken(Landlord landlord)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", landlord.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
