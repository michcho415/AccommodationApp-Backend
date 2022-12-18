using FluentValidation;

namespace DTO.InputDTOs
{
    public class LoginDTO
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public class LoginDTOValidator : AbstractValidator<LoginDTO>
        {
            public LoginDTOValidator()
            {
                RuleFor(x => x.Login).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
    }
}
