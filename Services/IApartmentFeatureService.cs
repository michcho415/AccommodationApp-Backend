using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IApartmentFeatureService
    {
        public Task<IActionResult> AddNewFeatureAsync(ApartmentFeatureDTO apartmentFeatureDTO);

        public Task<ICollection<ApartmentFeature>?> GetListOfFeaturesForApartment(int apartmentID);

        public Task<IActionResult> AddFeaturesToApartment(AddFeatureToApartmentDTO addFeatureToApartmentDTO);

        public Task<ApartmentsFeaturesListDto> GetListOfAllFeatures(PaginationDTO paginationDTO);
    }
}
