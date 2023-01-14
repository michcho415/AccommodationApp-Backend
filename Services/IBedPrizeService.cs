using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services
{
    public interface IBedPrizeService
    {
        public Task<BedPrize?> GetAsync(int id);
        public Task<IActionResult> CreateAsync(BedPrizeDTO bedPrizeDto);
        public Task<IActionResult> DeleteAsync(int id);
    }
}
