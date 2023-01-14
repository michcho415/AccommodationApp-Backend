using FluentValidation;

namespace DTO.InputDTOs
{
    public class BedPrizeDTO
    {
        public int BedsCount { get; set; }
        public decimal Prize { get; set; }
        public int ApartmentId { get; set; }

        public class BedPrizeDTOValidator: AbstractValidator<BedPrizeDTO>
        {
            public BedPrizeDTOValidator()
            {
                RuleFor(x => x.BedsCount).NotEmpty().GreaterThanOrEqualTo(1);
                RuleFor(x => x.Prize).NotEmpty().GreaterThan(0.0M);
                RuleFor(x => x.ApartmentId).NotEmpty();
            }
        }
    }
}
