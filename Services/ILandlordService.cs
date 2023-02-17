using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services
{
    public interface ILandlordService
    {
        public Task<LoginResponseDTO?> AuthenticateAsync(LoginDTO login);
        public Task<IActionResult> CreateAsync(LandlordDTO landlord);
        public Task<Landlord?> GetAsync(int id);
        public Task<IActionResult> DeleteAsync(int id);
        public Task<ICollection<Landlord>?> GetAll();
    }
}
