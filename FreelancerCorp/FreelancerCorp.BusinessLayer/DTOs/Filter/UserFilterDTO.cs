using FreelancerCorp.BusinessLayer.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class UserFilterDTO : FilterDTOBase
    {
        public string UserName { get; set; }
        public int? UserId { get; set; }
    }
}
