using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.InputDTOs
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string? Phone { get; set; }

        public string EmailAddress { get; set; }

        public bool IsAdmin { get; set; }
    }

    public class UserDTOValidator : AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }
}
