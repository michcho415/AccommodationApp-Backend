using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; }
        public int NumberOfElements{ get; set; }
    }

    public class PaginationDTOValidator: AbstractValidator<PaginationDTO>
    {
        public PaginationDTOValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.NumberOfElements).GreaterThanOrEqualTo(10).LessThanOrEqualTo(100);
        }
    }
}
