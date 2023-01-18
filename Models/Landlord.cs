using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Landlord
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(84)]
        public string Password { get; set; }

        [MaxLength(14)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        [Required]
        public bool ActiveFlag { get; set; } = false;

        public ICollection<Apartment> Apartments { get; set; }

    }
}
