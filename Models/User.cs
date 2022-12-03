using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [MaxLength(12)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public UserType UserType { get; set; }

        public ICollection<Rental> Rentals { get; set; }

        public ICollection<Apartment> Apartments { get; set; }

    }
}
