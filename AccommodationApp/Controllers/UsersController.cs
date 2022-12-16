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
    }
}
