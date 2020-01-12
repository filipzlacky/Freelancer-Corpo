using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Common;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class FreelancerDTO : DTOBase
    {
        public string Name { get; set; }  
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Sex Sex { get; set; }
        public string Location { get; set; }

        public DateTime DoB { get; set; }
        public string Info { get; set; }

        public double? SumRating { get; set; }
        
        public int RatingCount { get; set; }

        public List<OfferDTO> Offers { get; set; } = new List<OfferDTO>();
        public List<OfferDTO> AppliedToOffers { get; set; } = new List<OfferDTO>();

        public List<(RatingDTO rating, string creator)> Ratings { get; set; } = new List<(RatingDTO rating, string creator)>();

        public int GetAge()
        {
            int age = 0;
            age = DateTime.Now.Year - DoB.Year;
            if (DateTime.Now.DayOfYear < DoB.DayOfYear)
                age = age - 1;

            return age;
        }

    }
}
