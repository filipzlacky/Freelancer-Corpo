using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class CorporationFilterDTO
    {
        public int[] CorporationIds { get; set; }

        public string[] CorporationNames { get; set; }
        public string SearchedName { get; set; }

        public string SearchedLocation { get; set; }

        public int AvgRating { get; set; }
    }
}
