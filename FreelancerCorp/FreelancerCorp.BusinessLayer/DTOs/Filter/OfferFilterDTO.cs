using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancerCorp.BusinessLayer.DTOs.Filter
{
    public class OfferFilterDTO : FilterDTOBase
    {
        public int[] SearchedAuthorsIds { get; set; }
        public int[] SearchedAppliersIds { get; set; }
        public string SearchedName { get; set; }
        public Category? SearchedCategory { get; set; }

        public int? SearchedPrice { get; set; }
    }
}
