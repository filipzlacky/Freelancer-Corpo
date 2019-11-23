﻿using FreelancerCorp.BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferCreateViewModel
    {
        public Category Category { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }

        public string Details { get; set; }

        public int CreatorId { get; set; }
    }
}