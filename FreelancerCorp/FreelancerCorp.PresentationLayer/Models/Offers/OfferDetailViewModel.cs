using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferDetailViewModel
    {
        public OfferDTO Offer { get; set; }
        public (string name, int id) Creator { get; set; }
        public (string name, int? id) Applier { get; set; }
    }
}