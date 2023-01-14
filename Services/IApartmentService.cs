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
    public interface IApartmentService
    {

        public Task<Apartment?> GetAsync(int id);
        public Task<IActionResult> CreateAsync(ApartmentDTO apartmentDto);
        public Task<IActionResult> UpdateAsync(ApartmentDTO apartmentDto);
        public Task<IActionResult> DeleteAsync(int id);
        public Task<ApartmentsListDTO> GetAllAsync(PaginationDTO paginationDTO);
        public Task<ApartmentsListDTO> GetFilteredListAsync(ApartmentFiltersDTO apartmentFiltersDTO);
    }
}
