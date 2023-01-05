using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class ApartmentFeatureDTO
    {
        public string Name { get; set; }
    }

    public class ApartmentFeatureDTOValidator : AbstractValidator<ApartmentFeatureDTO> 
    {
        public ApartmentFeatureDTOValidator() 
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(30);
        }
    }
}
