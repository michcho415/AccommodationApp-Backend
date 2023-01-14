using DTO.InputDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class RentalService : IRentalService
    {
        private readonly IDbContextFactory<DatabaseContext> contextFactory;

        public RentalService(IDbContextFactory<DatabaseContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<Rental?> GetAsync(int id)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
                return await context.Rentals.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IActionResult> CreateAsync(RentalDTO rentalDto)
        {
            using (DatabaseContext context = contextFactory.CreateDbContext())
            {
                var bedPrize = await context.BedPrizes.FirstOrDefaultAsync(x => x.Id == rentalDto.PrizeId);
                if (bedPrize == null)
                    return new UnauthorizedObjectResult(rentalDto.PrizeId);

                var tenant = await context.Users.FirstOrDefaultAsync(x => x.ID == rentalDto.TenantId);
                if (tenant == null)
                    return new UnauthorizedObjectResult(rentalDto.TenantId);

                var rental = await context.Rentals.AnyAsync(x => x.Prize.Apartment == bedPrize.Apartment
                    && x.EndDate >= rentalDto.StartDate && x.StartDate <= rentalDto.EndDate);
                if (rental)
                    return new UnprocessableEntityObjectResult("Such rental already exists.");

                var newRental = new Rental()
                {
                    StartDate = rentalDto.StartDate,
                    EndDate = rentalDto.EndDate,
                    RentPrize = rentalDto.RentPrize,
                    Prize = bedPrize,
                    Tenant = tenant
                };

                await context.Rentals.AddAsync(newRental);
                await context.SaveChangesAsync();
            }
            return new OkResult();
        }
    }
}
