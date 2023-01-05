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
        public async Task<IActionResult> AddNewFeature(ApartmentFeatureDTO featureDTO)
        {
            return await apartmentFeaturesService.AddNewFeature(featureDTO);
        }

        [HttpGet("Apartment")]

        public async Task<ICollection<ApartmentFeature>?> GetListOfFeaturesForApartment([FromQuery]int apartmentID)
        {
            return await apartmentFeaturesService.GetListOfFeaturesForApartment(apartmentID);
        }

        [HttpPost("AddToApartment")]
        public async Task<IActionResult> AddFeaturesToApartment(AddFeatureToApartmentDTO addFeatureToApartmentDTO)
        {
            return await apartmentFeaturesService.AddFeaturesToApartment(addFeatureToApartmentDTO);
        }

        [HttpGet]
        public async Task<ApartmentsFeaturesListDto> GetListOfAllFeatures(PaginationDTO paginationDTO)
        {
            return await apartmentFeaturesService.GetListOfAllFeatures(paginationDTO);
        }
    }
}
