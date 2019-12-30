using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Ratings
{
    public class RatingCreateViewModel
    {
        public RatingDTO Rating { get; set; }
        public string RatedUserName { get; set; }
    }
}