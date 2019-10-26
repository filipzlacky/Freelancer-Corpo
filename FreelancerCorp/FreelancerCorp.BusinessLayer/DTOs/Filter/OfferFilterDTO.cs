using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class OfferFilterDTO
    {
        public Category SearchedCategory { get; set; }

        public int SearchedPrice { get; set; }
    }
}
