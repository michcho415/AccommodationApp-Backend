using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DTO.InputDTOs
{
    public class ExampleInputDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }

    }

    public class ExampleInputDTOValidator : AbstractValidator<ExampleInputDTO>
    {
        public ExampleInputDTOValidator()
        {
            RuleFor(x => x.ID).LessThan(2);
            RuleFor(x => x.Name).NotEmpty();
        }
    }

}
