using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferEditViewModel
    {
        public string Name { get; set; }
        public Category Category { get; set; }

        public string Description { get; set; }

        public long Price { get; set; }

        public string AdditionalInfo { get; set; }

        public int ApplierId { get; set; }
    }
}