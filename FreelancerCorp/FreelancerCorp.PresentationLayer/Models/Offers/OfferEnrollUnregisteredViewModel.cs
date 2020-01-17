using FreelancerCorp.BusinessLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferEnrollUnregisteredViewModel
    {
        public OfferDTO Offer { get; set; }
        public int OfferId { get; set; }
        public string Creator { get; set; }
        public int CreatorId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public string Info { get; set; }
    }
}