using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Password { get; set; }

        [MaxLength(12)]
        public string? Phone { get; set; }

        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; }

        public ICollection<Apartment> Apartments { get; set; }

    }
}
