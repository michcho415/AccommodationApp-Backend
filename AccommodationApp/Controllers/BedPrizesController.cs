using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedPrizesController : Controller
    {
        private readonly IBedPrizeService bedPrizeService;

        public BedPrizesController(IBedPrizeService bedPrizeService)
        {
            this.bedPrizeService = bedPrizeService;
        }

        [HttpGet("{id:int}")]
        public async Task<BedPrize?> GetAsync(int id)
        {
            return await bedPrizeService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(BedPrizeDTO bedPrizeDTO)
        {
            return await bedPrizeService.CreateAsync(bedPrizeDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await bedPrizeService.DeleteAsync(id);
        }
    }
}
