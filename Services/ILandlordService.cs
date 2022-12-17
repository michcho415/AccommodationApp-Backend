using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Services
{
    public interface ILandlordService
    {
        public Task<IActionResult> AuthenticateAsync(LoginDTO login);
        public Task<IActionResult> CreateAsync(LandlordDTO landlord);
        public Task<IActionResult> DeleteAsync(int id);
    }
}
