using DTO.InputDTOs;
using DTO.OutputDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ApartmentFeatureService : IApartmentFeatureService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;
        public ApartmentFeatureService(IDbContextFactory<DatabaseContext> contextFactory) 
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IActionResult> AddNewFeatureAsync(ApartmentFeatureDTO apartmentFeatureDTO)
        {
            using(DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                if (await ctx.ApartmentsFeatures
                    .FirstOrDefaultAsync(x => x.FeatureName.ToLower()
                    .Equals(apartmentFeatureDTO.Name.ToLower())) != null)
                    return new UnprocessableEntityObjectResult("This feature already exists.");

                await ctx.ApartmentsFeatures.AddAsync(
                new ApartmentFeature()
                {
                    FeatureName = apartmentFeatureDTO.Name
                });

                await ctx.SaveChangesAsync();
            }
            return new OkResult();
        }

        public async Task<ICollection<ApartmentFeature>?> GetListOfFeaturesForApartment(int apartmentID)
        {
            using(DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var apartment = await ctx.Apartments
                    .Include(x => x.ApartmentFeatures)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ID == apartmentID);
                return apartment?.ApartmentFeatures.ToList();
            }
        }

        public async Task<IActionResult> AddFeaturesToApartment(AddFeaturesToApartmentDTO addFeatureToApartmentDTO)
        {
            using (DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var apartment = await ctx.Apartments
                    .Include(x => x.ApartmentFeatures)
                    .FirstOrDefaultAsync(x => x.ID == addFeatureToApartmentDTO.ApartmentID);

                if (apartment == null)
                    return new UnprocessableEntityObjectResult("Apartment does not exist.");

                foreach(var id in addFeatureToApartmentDTO.ApartmentFeaturesIDs)
                {
                    var feature = await ctx.ApartmentsFeatures.FirstOrDefaultAsync(x => x.Id == id);
                    bool isAlreadyAdded = apartment.ApartmentFeatures.Any(x => x.Id == id);
                    if(feature != null && !isAlreadyAdded)
                        apartment.ApartmentFeatures.Add(feature);
                }

                await ctx.SaveChangesAsync();
            }
            return new OkResult();
        }

        public async Task<ApartmentsFeaturesListDto> GetListOfAllFeatures(PaginationDTO paginationDTO)
        {
            int page = paginationDTO.Page;
            int numberOfElements = paginationDTO.NumberOfElements;
            using (DatabaseContext ctx = contextFactory.CreateDbContext())
            {
                var features = await ctx.ApartmentsFeatures
                    .Skip((page - 1) * numberOfElements)
                    .Take(numberOfElements)
                    .AsNoTracking()
                    .ToListAsync();

                var featuresCount = await ctx.ApartmentsFeatures.CountAsync();

                return new ApartmentsFeaturesListDto()
                {
                    ApartmentsFeatures = features,
                    ApartmentsFeaturesCount = featuresCount
                };
            }
        }
    }
}
