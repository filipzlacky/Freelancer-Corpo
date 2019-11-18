using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class OfferDTO : DTOBase
    {
        public Category Cathegory { get; set; }
        
        public string Description { get; set; }
        
        public long Price { get; set; }

        public string AdditionalInfo { get; set; }
        
        public int CreatorId { get; set; }
    }
}
