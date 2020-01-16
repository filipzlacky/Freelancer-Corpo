using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferEnrollViewModel
    {
        public OfferDTO Offer { get; set; }
        public int OfferId { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }
        public string Applier { get; set; }
        public int ApplierId { get; set; }
    }
}