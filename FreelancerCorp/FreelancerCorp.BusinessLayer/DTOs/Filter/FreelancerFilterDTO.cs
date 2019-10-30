using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class FreelancerFilterDTO : FilterDTOBase
    {
        //public int[] FreelancerIds { get; set; }

        public List<string> FreelancerNames { get; set; } = new List<string>();

        public string SearchedName { get; set; }
        public string SearchedLocation { get; set; }
    }
}
