using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerCorp.BusinessLayer.DTOs.Enums;

namespace FreelancerCorp.BusinessLayer.DTOs
{
    public class CreateRatingDTO
    {
        public int RatedUserId { get; set; }

        public UserRole RatedUserRole { get; set; }

        public RatingDTO Rating { get; set; }
    }
}
