using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class ApartmentDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int MaxBedNumbers { get; set; }
        public string City { get; set; }
        public string? Street { get; set; }

        public int LandlordID { get; set; }

        public class ApartmentDTOValidator: AbstractValidator<ApartmentDTO>
        {
            public ApartmentDTOValidator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.LandlordID).NotNull();
                RuleFor(x => x.MaxBedNumbers).GreaterThanOrEqualTo(1);
            }
        }
    }
}
