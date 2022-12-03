using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BedPrize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BedsCount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Prize { get; set; }

        [Required]
        public Apartment Apartment { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
