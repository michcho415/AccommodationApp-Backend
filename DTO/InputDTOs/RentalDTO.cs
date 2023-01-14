using FluentValidation;

namespace DTO.InputDTOs
{
    public class RentalDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RentPrize { get; set; }
        public int PrizeId { get; set; }
        public int TenantId { get; set; }

        public class RentalDTOValidator : AbstractValidator<RentalDTO>
        {
            public RentalDTOValidator()
            {
                RuleFor(x => x.StartDate).NotEmpty().GreaterThan(DateTime.Now);
                RuleFor(x => x.EndDate).NotEmpty().GreaterThan(x => x.StartDate);
                RuleFor(x => x.RentPrize).NotEmpty().GreaterThan(0.0M);
                RuleFor(x => x.PrizeId).NotEmpty();
                RuleFor(x => x.TenantId).NotEmpty();
            }
        }
    }
}
