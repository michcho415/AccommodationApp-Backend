using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApartmentFeature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FeatureName { get; set; } = string.Empty;

        public ICollection<Apartment> Apartments { get; set; }
    }
}
