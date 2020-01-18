using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Ratings
{
    public class RatingViewModel
    {
        public RatingDTO Rating { get; set; }
        public string CreatorUserName { get; set; }
        public string RatedUserName { get; set; }
        public int RatedUserId { get; set; }
        public string RatedUserRole { get; set; }
    }
}