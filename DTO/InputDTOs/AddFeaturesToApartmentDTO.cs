using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class AddFeaturesToApartmentDTO
    {
        public List<int>? ApartmentFeaturesIDs { get; set; }
        public int ApartmentID { get; set; }
    }

    public class AddFeaturesToApartmentValidator : AbstractValidator<AddFeaturesToApartmentDTO>
    {
        public AddFeaturesToApartmentValidator() 
        {
            RuleFor(x => x.ApartmentFeaturesIDs).NotEmpty().NotNull();
            RuleFor(x => x.ApartmentID).NotNull();
        }
    }
}
