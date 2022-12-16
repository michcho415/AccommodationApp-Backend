using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;

        public UserService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IActionResult> CreateAsync(UserDTO user)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var result = await context.Users.AddAsync(new User()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Phone = user.Phone,
                    EmailAddress = user.EmailAddress,
                    IsAdmin = user.IsAdmin
                });
                await context.SaveChangesAsync();
                return new OkResult();
            }
        }
    }
}
