using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalsController : Controller
    {
        private readonly IRentalService rentalService;

        public RentalsController(IRentalService rentalService)
        {
            this.rentalService = rentalService;
        }

        [HttpGet("{id:int}")]
        public async Task<Rental?> GetAsync(int id)
        {
            return await rentalService.GetAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(RentalDTO rentalDTO)
        {
            return await rentalService.CreateAsync(rentalDTO);
        }
    }
}
