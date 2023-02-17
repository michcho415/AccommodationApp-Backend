using Models;

namespace DTO.OutputDTOs
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsLandlord { get; set; }
        public string Token { get; set; }

        public LoginResponseDTO(User user, string token)
        {
            Id = user.ID;
            Username = user.Username;
            IsAdmin = user.IsAdmin;
            IsLandlord = false;
            Token = token;
        }

        public LoginResponseDTO(Landlord landlord, string token)
        {
            Id = landlord.ID;
            Username = landlord.Username;
            IsAdmin = false;
            IsLandlord = true;
            Token = token;
        }
    }
}
