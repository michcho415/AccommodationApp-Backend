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
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;
        private readonly PasswordHasher<User> hasher = new();
        private readonly AppSettings appSettings;

        public UserService(IDbContextFactory<DatabaseContext> contextFactory, IOptions<AppSettings> appSettings)
        {
            this.contextFactory = contextFactory;
            this.appSettings = appSettings.Value;
        }

        public async Task<IActionResult> CreateAsync(UserDTO user)
        {
            var existingUser = await GetAsync(user.Username);
            if (existingUser != null)
                return new UnprocessableEntityResult();
            var existingEmail = await GetByEmailAsync(user.EmailAddress);
            if (existingEmail != null)
                return new UnprocessableEntityResult();

            var newUser = new User()
            {
                Username = user.Username,
                Phone = user.Phone,
                EmailAddress = user.EmailAddress,
                IsAdmin = user.IsAdmin
            };

            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var result = await context.Users.AddAsync(newUser);
                var hashedPassword = hasher.HashPassword(newUser, user.Password);
                newUser.Password = hashedPassword;

                await context.SaveChangesAsync();
            }
            return new OkResult();
        }

        private async Task<User?> GetAsync(string username)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Users.FindAsync(id);
        }

        private async Task<User?> GetByEmailAsync(string emailAddress)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Users.FirstOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var userToDelete = await context.Users.FindAsync(id);
                if (userToDelete != null)
                {
                    context.Users.Remove(userToDelete);
                    await context.SaveChangesAsync();
                }
            }
            return new OkResult();
        }

        public async Task<LoginResponseDTO?> AuthenticateAsync(LoginDTO login)
        {
            var user = await GetAsync(login.Login);
            if (user == null || hasher.VerifyHashedPassword(user, user.Password, login.Password) == PasswordVerificationResult.Failed)
                return null;

            var token = generateJwtToken(user);
            return new LoginResponseDTO(user, token);
        }

        public async Task<ICollection<User>?> GetAll()
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Users.ToListAsync();
        }

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.ID.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
