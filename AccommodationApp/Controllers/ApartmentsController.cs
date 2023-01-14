using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentsController : Controller
    {
        private readonly IApartmentService apartmentService;

        public ApartmentsController(IApartmentService apartmentService)
        {
            this.apartmentService = apartmentService;
        }

        [HttpGet("{id:int}")]
        public async Task<Apartment?> GetAsync(int id)
        {
            return await apartmentService.GetAsync(id);
        }

        [HttpPost("All")]
        public async Task<ApartmentsListDTO> GetAllAsync(PaginationDTO paginationDTO)
        {
            return await apartmentService.GetAllAsync(paginationDTO);
        }

        [HttpPost("Search")]
        public async Task<ApartmentsListDTO> GetFilteredResult(ApartmentFiltersDTO apartmentFiltersDTO)
        {
            return await apartmentService.GetFilteredListAsync(apartmentFiltersDTO);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(ApartmentDTO dto)
        {
            return await apartmentService.CreateAsync(dto);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            return await apartmentService.DeleteAsync(id);
        }
    }
}
