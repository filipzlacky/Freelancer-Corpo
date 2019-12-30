using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Corporations
{
    public class CorporationDetailViewModel
    {
        public CorporationDTO Corporation { get; set; }
        public string CorporationUserName { get; set; }
    }
}