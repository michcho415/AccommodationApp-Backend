using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Apartment
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public int MaxBedNumbers { get; set; }

        [Required]
        [MaxLength(40)]
        public string City { get; set; }

        [MaxLength(80)]
        public string Street { get; set; }

        [Required]
        public User Landlord { get; set; }

        public ICollection<BedPrize> BedPrizes { get; set; }

        public ICollection<ApartmentFeature> ApartmentFeatures { get; set; }

    }
}
