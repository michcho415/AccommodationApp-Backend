using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class ApartmentFiltersDTO
    {
        public string? Name { get; set; }

        public int? BedNumbers { get; set; }

        public string? City { get; set; }
        public PaginationDTO PaginationDTO { get; set; }
    }

    public class ApartmentFiltersDTOValidator : AbstractValidator<ApartmentFiltersDTO>
    {
        public ApartmentFiltersDTOValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50).When(x => x.Name != null);
            RuleFor(x => x.City).MaximumLength(30).When(x => x.City != null);
            RuleFor(x => x.BedNumbers).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PaginationDTO).SetValidator(new PaginationDTOValidator());
        }
    }

}
