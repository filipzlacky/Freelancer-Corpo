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

        public int? SearchedAverage { get; set; }

        public string SearchedFreelancerName { get; set; }

        public string SearchedUserName { get; set; }

        public string SearchedLocation { get; set; }

        public string SearchedSex { get; set; }

        public string UserRole { get; set; } = "Freelancer";
    }
}
