using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Accounts
{
    public class FreelancerProfileModel
    {
        public FreelancerDTO FreelancerDTO { get; set; }
        public string UserName { get; set; }
    }
}