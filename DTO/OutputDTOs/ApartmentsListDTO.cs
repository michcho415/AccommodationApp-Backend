using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.OutputDTOs
{
    public class ApartmentsListDTO
    {
        public ICollection<Apartment>? Apartments { get; set; }

        public int NumberOfApartments { get; set; }
    }
}
