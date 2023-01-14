using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Services
{
    public interface IRentalService
    {
        public Task<Rental?> GetAsync(int id);
        public Task<IActionResult> CreateAsync(RentalDTO rentalDto);
    }
}
