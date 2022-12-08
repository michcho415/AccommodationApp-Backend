﻿using System.ComponentModel.DataAnnotations;

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
        public bool IsAdmin { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
