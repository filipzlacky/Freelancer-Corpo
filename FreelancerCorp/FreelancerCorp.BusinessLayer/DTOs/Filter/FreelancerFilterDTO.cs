using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class FreelancerFilterDTO
    {
        public int[] FreelancerIds { get; set; }

        public string[] FreelancerNames { get; set; }
        public string SearchedName { get; set; }

        public DateTime SearchedDoB { get; set; }

        public string SearchedLocation { get; set; }

        public Sex SearchedSex { get; set; }

        public int AvgRating { get; set; }
    }
}
