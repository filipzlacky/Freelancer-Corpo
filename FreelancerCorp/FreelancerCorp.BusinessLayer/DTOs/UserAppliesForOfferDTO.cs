using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class UserAppliesForOfferDTO
    {
        public int ApplierId { get; set; }
        
        public OfferDTO Offer { get; set; }
    }
}
