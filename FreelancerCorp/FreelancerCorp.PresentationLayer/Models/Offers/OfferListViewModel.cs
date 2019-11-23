using FreelancerCorp.BusinessLayer.DTOs;
using FreelancerCorp.BusinessLayer.DTOs.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FreelancerCorp.PresentationLayer.Models.Offers
{
    public class OfferListViewModel
    {
        public List<string> SortCriteria => new List<string> { nameof(OfferDTO.Cathegory), nameof(OfferDTO.Price) };

        public List<OfferDTO> Offers { get; set; }

        public OfferFilterDTO Filter { get; set; }
    }
}