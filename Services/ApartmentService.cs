using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;

        public ApartmentService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Apartment?> GetAsync(int id)
        {
            using (DatabaseContext ctx = contextFactory.CreateDbContext())
                return await ctx.Apartments
                    .Include(x => x.BedPrizes)
                    .Include(x => x.ApartmentFeatures)
                    .FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IActionResult> CreateAsync(ApartmentDTO apartmentDto)
        {
            using(DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var landlord = await ctx.Landlords.FirstOrDefaultAsync(x => x.ID == apartmentDto.LandlordID);

                if (landlord == null)
                    return new UnauthorizedObjectResult(apartmentDto.LandlordID);

                //check if apartments with the same name and city already exists
                if (ctx.Apartments.Any(x => x.Name == apartmentDto.Name && x.City == apartmentDto.City))
                    return new UnprocessableEntityObjectResult("Such apartment already exists.");

                var newApartment = new Apartment()
                {
                    Name = apartmentDto.Name,
                    Description = apartmentDto.Description,
                    MaxBedNumbers = apartmentDto.MaxBedNumbers,
                    City = apartmentDto.City,
                    Street = apartmentDto.Street,
                    Landlord = landlord,
                };

                await ctx.Apartments.AddAsync(newApartment);
                await ctx.SaveChangesAsync();

                return new OkResult();
                
            }
        }

        public async Task<IActionResult> UpdateAsync(ApartmentDTO apartmentDto)
        {
            using (DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var apartment = await ctx.Apartments.FirstOrDefaultAsync(x => x.ID == apartmentDto.ID);

                if (apartment == null)
                    return new UnprocessableEntityObjectResult("Apartment does not exist.");

                apartment.Street = apartmentDto.Street;
                apartment.Description = apartmentDto.Description;
                apartment.Name = apartmentDto.Name;
                apartment.City = apartmentDto.City;

                ctx.Apartments.Update(apartment);

                await ctx.SaveChangesAsync();

                return new OkResult();
            }
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            using(DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                //TO DO: check if user that makes request has permission

                var apartment = await ctx.Apartments.FirstOrDefaultAsync(x => x.ID == id);

                if (apartment == null)
                    return new UnprocessableEntityObjectResult("Apartment does not exist.");

                if(apartment.BedPrizes != null)
                    apartment.BedPrizes.Clear();

                ctx.Apartments
                    .Where(x => x.ID == id)
                    .ExecuteDelete();

                await ctx.SaveChangesAsync();

                return new OkResult();
            }
        }

        public async Task<ApartmentsListDTO> GetAllAsync(PaginationDTO paginationDTO)
        {
            var page = paginationDTO.Page;
            var numberOfElements = paginationDTO.NumberOfElements;
            using(DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var apartments = await ctx.Apartments
                    .Skip((page - 1) * numberOfElements)
                    .Take(numberOfElements)
                    .AsNoTracking()
                    .ToListAsync();

                var apartmentsCount = await ctx.Apartments
                    .AsNoTracking()
                    .CountAsync();

                return new ApartmentsListDTO()
                {
                    Apartments = apartments,
                    NumberOfApartments = apartmentsCount
                };
            }
        }

        public async Task<ApartmentsListDTO> GetFilteredListAsync(ApartmentFiltersDTO apartmentFiltersDTO)
        {
            var page = apartmentFiltersDTO.PaginationDTO.Page;
            var numberOfElements = apartmentFiltersDTO.PaginationDTO.NumberOfElements;
            using (DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                //apply filters on query
                var apartmentsQuery = ctx.Apartments
                    .Where(x => x.Name.Contains(apartmentFiltersDTO.Name ?? ""))
                    .Where(x => x.City.Contains(apartmentFiltersDTO.City ?? ""));

                if(apartmentFiltersDTO.BedNumbers != null)
                    apartmentsQuery.Where(x => x.MaxBedNumbers < apartmentFiltersDTO.BedNumbers);

                var apartments = await apartmentsQuery
                    .Skip((page - 1) * numberOfElements)
                    .Take(numberOfElements)
                    .AsNoTracking()
                    .ToListAsync();

                var apartmentsCount = await apartmentsQuery
                    .AsNoTracking()
                    .CountAsync();

                return new ApartmentsListDTO()
                {
                    Apartments = apartments,
                    NumberOfApartments = apartmentsCount
                };
            }
        }

    }
}
