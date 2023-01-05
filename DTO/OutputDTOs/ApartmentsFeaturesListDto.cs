using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DTO.OutputDTOs
{
    public class ApartmentsFeaturesListDto
    {
        public ICollection<ApartmentFeature>? ApartmentsFeatures { get; set; }
        public int ApartmentsFeaturesCount { get; set; }
    }
}
