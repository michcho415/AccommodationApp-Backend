using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services
{
    public interface IUserService
    {
        public Task<LoginResponseDTO?> AuthenticateAsync(LoginDTO login);
        public Task<IActionResult> CreateAsync(UserDTO user);
        public Task<User?> GetAsync(int id);
        public Task<IActionResult> DeleteAsync(int id);
        public Task<ICollection<User>?> GetAll();
    }
}
