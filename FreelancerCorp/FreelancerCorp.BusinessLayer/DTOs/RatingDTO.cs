using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class RatingDTO : DTOBase
    {        
        public int CreatorId { get; set; }

        public string Comment { get; set; }

        public int Score { get; set; }
    }
}
