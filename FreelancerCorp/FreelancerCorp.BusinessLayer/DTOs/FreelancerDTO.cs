using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class FreelancerDTO : DTOBase
    {
        public string Name { get; set; }

        public IEnumerable<OfferDTO> Products { get; set; } = new List<OfferDTO>();
    }
}
