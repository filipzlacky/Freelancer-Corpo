using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Freelancers
{
    public class FreelancerDetailViewModel
    {
        public FreelancerDTO Freelancer { get; set; }
        public string UserName { get; set; }
    }
}