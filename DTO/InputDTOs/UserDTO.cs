﻿using FluentValidation;

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
            RuleFor(x => x.Phone).Matches("^\\+?[1-9][0-9]{7,14}$").When(x => !string.IsNullOrEmpty(x.Phone));
            RuleFor(x => x.EmailAddress).NotEmpty().EmailAddress();
        }
    }
}
