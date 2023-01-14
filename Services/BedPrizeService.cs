using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class BedPrizeService : IBedPrizeService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;

        public BedPrizeService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<BedPrize?> GetAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.BedPrizes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IActionResult> CreateAsync(BedPrizeDTO bedPrizeDto)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var apartment = await context.Apartments.FirstOrDefaultAsync(x => x.ID == bedPrizeDto.ApartmentId);

                if (apartment == null)
                    return new UnauthorizedObjectResult(bedPrizeDto.ApartmentId);

                var bedPrice = await context.BedPrizes.AnyAsync(x => x.Id == bedPrizeDto.ApartmentId && x.BedsCount == bedPrizeDto.BedsCount);
                if (bedPrice)
                    return new UnprocessableEntityObjectResult("Such bed prize already exists.");

                var newBedPrice = new BedPrize()
                {
                    BedsCount = bedPrizeDto.BedsCount,
                    Prize = bedPrizeDto.Prize,
                    Apartment = apartment
                };

                await context.BedPrizes.AddAsync(newBedPrice);
                await context.SaveChangesAsync();
            }
            return new OkResult();
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                //TO DO: check if user that makes request has permission

                var bedPrize = await context.BedPrizes.FirstOrDefaultAsync(x => x.Id == id);

                if (bedPrize == null)
                    return new UnprocessableEntityObjectResult("Bed prize does not exist.");

                context.BedPrizes.Remove(bedPrize);

                await context.SaveChangesAsync();
            }
            return new OkResult();
        }
    }
}
