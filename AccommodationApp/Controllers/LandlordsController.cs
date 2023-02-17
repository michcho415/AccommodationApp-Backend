using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LandlordsController : Controller
    {
        private readonly ILandlordService _landlordService;

        public LandlordsController(ILandlordService landlordService)
        {
            _landlordService = landlordService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(LandlordDTO landlord)
        {
            return await _landlordService.CreateAsync(landlord);
        }

        [HttpPost("authenticate")]
        public async Task<LoginResponseDTO?> AuthenticateAsync(LoginDTO login)
        {
            return await _landlordService.AuthenticateAsync(login);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await _landlordService.DeleteAsync(id);
        }

        [Authorize]
        [HttpGet]
        public async Task<ICollection<Landlord>?> GetAll()
        {
            return await _landlordService.GetAll();
        }
    }
}
