using DTO.InputDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;
        private readonly PasswordHasher<User> hasher = new();

        public UserService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IActionResult> CreateAsync(UserDTO user)
        {
            var existingUser = await GetAsync(user.Username);
            if (existingUser != null)
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

        public async Task<IActionResult> AuthenticateAsync(LoginDTO login)
        {
            var user = await GetAsync(login.Login);
            if (user == null || hasher.VerifyHashedPassword(user, user.Password, login.Password) == PasswordVerificationResult.Failed)
                return new UnprocessableEntityResult();
            return new OkResult();
        }
    }
}
