using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO user)
        {
            return await _userService.CreateAsync(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(LoginDTO login)
        {
            return await _userService.AuthenticateAsync(login);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await _userService.DeleteAsync(id);
        }
    }
}
