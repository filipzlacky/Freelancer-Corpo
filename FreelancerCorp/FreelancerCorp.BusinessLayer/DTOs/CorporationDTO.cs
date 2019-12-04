using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class CorporationDTO : DTOBase
    {
        public string Name { get; set; }
        public string Info { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public double Average { get; set; }

        public List<OfferDTO> Offers { get; set; } = new List<OfferDTO>();

        public List<RatingDTO> Ratings { get; set; } = new List<RatingDTO>();
    }
}
