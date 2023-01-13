using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace AccommodationApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentFeaturesController : Controller
    {
        private readonly IApartmentFeatureService apartmentFeaturesService;

        public ApartmentFeaturesController(IApartmentFeatureService apartmentFeatureService)
        {
            this.apartmentFeaturesService = apartmentFeatureService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFeatureAsync(ApartmentFeatureDTO featureDTO)
        {
            return await apartmentFeaturesService.AddNewFeatureAsync(featureDTO);
        }

        [HttpGet("Apartment")]

        public async Task<ICollection<ApartmentFeature>?> GetListOfFeaturesForApartmentAsync([FromQuery]int apartmentID)
        {
            return await apartmentFeaturesService.GetListOfFeaturesForApartmentAsync(apartmentID);
        }

        [HttpPost("AddToApartment")]
        public async Task<IActionResult> AddFeaturesToApartmentAsync(AddFeaturesToApartmentDTO addFeatureToApartmentDTO)
        {
            return await apartmentFeaturesService.AddFeaturesToApartmentAsync(addFeatureToApartmentDTO);
        }

        [HttpGet]
        public async Task<ApartmentsFeaturesListDto> GetListOfAllFeaturesAsync(PaginationDTO paginationDTO)
        {
            return await apartmentFeaturesService.GetListOfAllFeaturesAsync(paginationDTO);
        }
    }
}
